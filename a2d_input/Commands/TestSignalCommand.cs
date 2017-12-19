using System;
using a2d_input.core;
using a2d_input.core.Enumerators;
using CommandLine;

namespace a2d_input.Commands
{
    [Verb("test", HelpText = "Test the level of the input signal.")]
    internal class TestSignalCommand : CaptureBaseCommand
    {
        public static int Run(TestSignalCommand command)
        {
            var device = DeviceEnumerator.GetWaveInDeviceById(command.DeviceId);
            if (device == null) throw new InvalidOperationException($"Device '{command.DeviceId}' is not found");

            using (var input = new WaveInput(device))
            {
                input.OnDataReady += OnDataReady;
                input.Start();

                while (true)
                {
                    var keyInfo = Console.ReadKey();
                    if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control) && keyInfo.Key == ConsoleKey.C)
                    {
                        Console.WriteLine("Closing...");
                        break;
                    }
                }

                input.Stop();
            }

            return 0;
        }

        private static void OnDataReady(IAudioData audioData)
        {
        }
    }
}