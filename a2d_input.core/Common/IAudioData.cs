using NAudio.Wave;

namespace a2d_input.core
{
    public interface IAudioData
    {
        byte[] Data { get; }
        WaveFormat WaveFormat { get; }
    }
}