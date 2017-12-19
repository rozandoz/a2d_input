using System;
using System.Threading;
using System.Threading.Tasks;
using a2d_input.core;
using a2d_input.core.Enumerators;
using a2d_input.core.Extensions;
using NAudio.Wave;

namespace a2d_input.Commands
{
    internal class TestSignalHandler : CommandHandler<TestSignalOptions>
    {
        private readonly Task _captureThread;
        private readonly CancellationTokenSource _tokenSource;

        private float _maxValue;
        private float _minValue;

        public TestSignalHandler(TestSignalOptions options)
            : base(options)
        {
            _tokenSource = new CancellationTokenSource();
            _captureThread = new Task(OnThreadProc, _tokenSource.Token, TaskCreationOptions.LongRunning);
        }

        private void OnThreadProc()
        {
            var token = _tokenSource.Token;

            var device = DeviceEnumerator.GetWaveInDeviceById(Options.DeviceId);
            if (device == null) throw new InvalidOperationException($"Device '{Options.DeviceId}' is not found");

            var waveFormat = new WaveFormat(44100, 16, 2);

            using (var input = new WaveInput(device, waveFormat))
            {
                input.OnDataReady += OnDataReady;
                input.Start();

                Console.WriteLine($"Wave input is started ({waveFormat})");

                while (!token.IsCancellationRequested)
                {
                    Thread.Sleep(10);
                }

                input.Stop();
            }
        }


        private void OnDataReady(IAudioData audioData)
        {
            var channel = Options.Channel;

            var bytes = audioData.Data;
            var waveFormat = audioData.WaveFormat;

            if (waveFormat.BitsPerSample != 16 || waveFormat.Channels != 2)
                throw new NotSupportedException("Format is not supported");

            var samples = bytes.ToInt16();

            short maxValue = 0;
            short minValue = 0;

            for (var i = channel; i < samples.Length - channel; i += 2)
            {
                var sample = samples[i];

                maxValue = Math.Max(sample, maxValue);
                minValue = Math.Min(sample, minValue);
            }

            var min = (float) minValue/short.MinValue;
            var max = (float) maxValue/short.MaxValue;

            if (Math.Abs(min - _minValue) >= 0.01 || Math.Abs(max - _maxValue) >= 0.01)
            {
                _minValue = min;
                _maxValue = max;

                Console.WriteLine($"Min: -{min:F2} \t Max: {max:F2}");
            }
        }

        protected override int OnRun(TestSignalOptions options)
        {
            _captureThread.Start();

            while (true)
            {
                var keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }

            Console.WriteLine("Closing wave input...");

            _tokenSource.Cancel();
            _captureThread.Wait();

            Console.WriteLine("Wave input is closed.");

            return 0;
        }
    }
}