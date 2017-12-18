using System;
using a2d_input.core.Enumerators;
using CommandLine;

namespace a2d_input.Commands
{
    [Verb("devices", HelpText = "Get list of input audio devices.")]
    public class GetDevicesCommand
    {
        public static void Run(GetDevicesCommand command)
        {
            var devices = DeviceEnumerator.GetWaveInDevices();

            foreach (var device in devices)
                Console.WriteLine(device.ToString());
        }
    }
}