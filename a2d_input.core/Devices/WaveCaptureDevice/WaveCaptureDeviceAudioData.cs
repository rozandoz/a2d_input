using System;
using NAudio.Wave;

namespace a2d_input.core
{
    public class WaveCaptureDeviceAudioData : IAudioData
    {
        private readonly WaveInEventArgs _waveInEventArgs;

        public WaveCaptureDeviceAudioData(WaveInEventArgs waveInArgs, WaveFormat waveFormat)
        {
            _waveInEventArgs = waveInArgs ?? throw new ArgumentNullException(nameof(waveInArgs));
            WaveFormat = waveFormat ?? throw new ArgumentNullException(nameof(waveFormat));
        }


        public byte[] Data => _waveInEventArgs.Buffer;
        public WaveFormat WaveFormat { get; }
    }
}