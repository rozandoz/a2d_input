using CommandLine;

namespace a2d_input.Commands
{
    [Verb("devices", HelpText = "Get list of input audio devices.")]
    public class GetDevicesOptions : CommandOptions
    {
    }
}