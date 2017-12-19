using CommandLine;

namespace a2d_input.Commands
{
    internal class CaptureBaseCommand
    {
        private const string DeviceHelpText = "The number of the input audio device. " +
                                              "To get a list of the available inputs 'devices' command can be used.";

        private const string ChannelHelpText = "The audio channel which must be captured.";

        [Option('d', "device", Required = true, HelpText = DeviceHelpText)]
        public int DeviceId { get; set; }

        [Option('c', "channel", Required = true, HelpText = ChannelHelpText)]
        public int Channel { get; set; }
    }
}