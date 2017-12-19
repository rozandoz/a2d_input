using System;
using a2d_input.core.Enumerators;

namespace a2d_input.Commands
{
    public class GetDevicesHandler : CommandHandler<GetDevicesOptions>
    {
        public GetDevicesHandler(GetDevicesOptions options)
            : base(options)
        {
        }

        protected override int OnRun(GetDevicesOptions options)
        {
            var devices = DeviceEnumerator.GetWaveInDevices();

            foreach (var device in devices)
                Console.WriteLine(device.ToString());

            return 0;
        }
    }
}