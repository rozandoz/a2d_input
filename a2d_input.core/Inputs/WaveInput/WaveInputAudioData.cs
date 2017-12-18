using System;
using NAudio.Wave;

namespace a2d_input.core
{
    public class WaveInputAudioData : IAudioData
    {
        public WaveInputAudioData(WaveInEventArgs waveInArgs, WaveFormat waveFormat)
        {
            if (waveInArgs == null) throw new ArgumentNullException(nameof(waveInArgs));
            if (waveFormat == null) throw new ArgumentNullException(nameof(waveFormat));

            WaveFormat = waveFormat;
            WaveInEventArgs = waveInArgs;
        }

        private WaveInEventArgs WaveInEventArgs { get; }

        public byte[] Data => WaveInEventArgs.Buffer;

        public WaveFormat WaveFormat { get; }
    }
}