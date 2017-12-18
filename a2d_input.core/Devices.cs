using System;
using System.Collections.Generic;
using NAudio.Wave;

namespace a2d_input.core
{
    public static class Devices
    {
        public static IReadOnlyList<Tuple<int, WaveInCapabilities>> GetWaveInDevices()
        {
            var devices = new List<Tuple<int, WaveInCapabilities>>();
            for (int deviceId = 0; deviceId < WaveIn.DeviceCount; deviceId++)
            {
                devices.Add(Tuple.Create(deviceId, WaveIn.GetCapabilities(deviceId)));
            }

            return devices.AsReadOnly();
        }
    }
}