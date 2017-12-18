using System;
using System.Diagnostics;
using System.Linq;
using a2d_input.core.Enumerators;
using NAudio.Wave;

namespace a2d_input.core
{
    public class WaveInput : IDisposable
    {
        private readonly WaveInputDevice _device;
        private readonly object _locker;

        private bool _isStarted;
        private WaveIn _waveIn;

        public WaveInput(WaveInputDevice device)
        {
            if (device == null) throw new ArgumentNullException(nameof(device));

            var devices = DeviceEnumerator.GetWaveInDevices();
            if (devices.All(d => d.DeviceId != device.DeviceId))
                throw new ArgumentException("Cannot find specified device");

            Debug.WriteLine(devices.ToString());

            _locker = new object();
            _device = device;

            _isStarted = false;
        }

        public Action<IAudioData> OnDataReady { get; set; }

        public void Dispose()
        {
            Stop();
        }

        public void Start()
        {
            lock (_locker)
            {
                Debug.WriteLine("Starting audio input...");

                if (_isStarted) throw new InvalidOperationException("The device is already started");

                _isStarted = false;

                _waveIn = new WaveIn
                {
                    DeviceNumber = _device.DeviceId,
                    WaveFormat = new WaveFormat()
                };

                _waveIn.DataAvailable += WaveInOnDataAvailable;
                _waveIn.StartRecording();

                _isStarted = true;
            }
        }

        public void Stop()
        {
            lock (_locker)
            {
                Debug.WriteLine("Stopping audio input...");

                if (!_isStarted) return;

                _isStarted = false;

                _waveIn.StopRecording();
                _waveIn.DataAvailable -= WaveInOnDataAvailable;
                _waveIn.Dispose();

                _waveIn = null;
            }
        }

        private void WaveInOnDataAvailable(object sender, WaveInEventArgs args)
        {
            var dataReady = OnDataReady;
            var audioData = new WaveInputAudioData(args, _waveIn.WaveFormat);

            dataReady?.Invoke(audioData);
        }
    }
}