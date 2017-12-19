using System;
using System.Threading;
using System.Threading.Tasks;
using a2d_input.core;
using a2d_input.core.Enumerators;

namespace a2d_input.Commands
{
    internal class TestSignalHandler : CommandHandler<TestSignalOptions>
    {
        private readonly Task _captureThread;
        private readonly CancellationTokenSource _tokenSource;

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

            using (var input = new WaveInput(device))
            {
                input.OnDataReady += OnDataReady;
                input.Start();

                while (!token.IsCancellationRequested)
                {
                    Thread.Sleep(10);
                }

                input.Stop();
            }
        }


        private static void OnDataReady(IAudioData audioData)
        {
        }

        protected override int OnRun(TestSignalOptions options)
        {
            _captureThread.Start();

            while (true)
            {
                var keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("Closing...");
                    break;
                }
            }

            _tokenSource.Cancel();
            _captureThread.Wait();

            return 0;
        }
    }
}