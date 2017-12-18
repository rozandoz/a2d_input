using CommandLine;

namespace a2d_input.Commands
{
    [Verb("run", HelpText = "Run, hooker... Run!")]
    public class RunHookerCommand
    {
        [Option('d', "device", Required = true, HelpText = "Input audio device.")]
        public int DeviceId { get; set; }

        [Option('c', "channel", Required = true, HelpText = "Channel number to track.")]
        public int Channel { get; set; }

        [Option('b', "Barrie", Required = true, HelpText = "Noise barrier.")]
        public float NoiseBarrier { get; set; }
    }
}