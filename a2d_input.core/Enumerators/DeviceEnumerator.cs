using System.Collections.Generic;
using NAudio.Wave;

namespace a2d_input.core.Enumerators
{
    public static class DeviceEnumerator
    {
        public static IReadOnlyList<WaveInputDevice> GetWaveInDevices()
        {
            var devices = new List<WaveInputDevice>();
            for (var deviceId = 0; deviceId < WaveIn.DeviceCount; deviceId++)
                devices.Add(new WaveInputDevice(deviceId, WaveIn.GetCapabilities(deviceId)));

            return devices.AsReadOnly();
        }
    }
}