using CommandLine;

namespace a2d_input.Commands
{
    [Verb("test", HelpText = "Test the level of the input signal.")]
    internal class TestSignalOptions : CaptureBaseCommand
    {
    }
}