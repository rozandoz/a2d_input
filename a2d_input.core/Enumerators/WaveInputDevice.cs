using NAudio.Wave;

namespace a2d_input.core.Enumerators
{
    public class WaveInputDevice
    {
        public WaveInputDevice(int deviceId, WaveInCapabilities capabilities)
        {
            DeviceId = deviceId;
            Capabilities = capabilities;
        }

        public int DeviceId { get; set; }

        public WaveInCapabilities Capabilities { get; set; }

        public override string ToString()
        {
            return $"Device {DeviceId}: {Capabilities.ProductName} (channels: {Capabilities.Channels})";
        }
    }
}