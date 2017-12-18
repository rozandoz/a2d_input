using System;
using System.Diagnostics;
using System.Linq;
using NAudio.Wave;

namespace a2d_input.core
{
    public class WaveCaptureDevice : IDisposable
    {
        private readonly int _deviceId;
        private readonly object _locker;

        private bool _isStarted;
        private WaveIn _waveIn;

        public WaveCaptureDevice(int deviceId)
        {
            var devices = Devices.GetWaveInDevices();

            var device = devices.FirstOrDefault(d => d.Item1 == deviceId);
            if (device == null) throw new ArgumentException("Cannot find specified device");

            var capabilities = device.Item2;
            Debug.WriteLine($"Device {deviceId}: {capabilities.ProductName} (channels: {capabilities.Channels})");

            _locker = new object();
            _deviceId = deviceId;

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
                Debug.WriteLine("Starting audio capture device...");

                if (_isStarted) throw new InvalidOperationException("The device is already started");

                _isStarted = false;

                _waveIn = new WaveIn
                {
                    DeviceNumber = _deviceId,
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
                Debug.WriteLine("Stopping audio capture device...");

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
            var audioData = new WaveCaptureDeviceAudioData(args, _waveIn.WaveFormat);

            dataReady?.Invoke(audioData);
        }
    }
}