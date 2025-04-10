using Pastel;
using System.Drawing;
using UsbForensics.Interfaces;
using UsbForensics.Services;

namespace UsbForensics.Scanners
{
    public class UsbEnumerationScanner
    {
        private readonly IRegistryRoot root;

        public UsbEnumerationScanner(IRegistryRoot registryRoot)
        {
            root = registryRoot;
        }

        public List<IRegistryDevice> Scan()
        {
            var systemKey = root.GetRegistry(@"System\CurrentControlSet\Enum\USB");
            List<IRegistryDevice> devices = new List<IRegistryDevice>();

            Console.WriteLine($"Looking for devices in: {systemKey.Name}".PastelBg(Color.White).Pastel(Color.Black));
            foreach (var item in systemKey.GetSubKeyNames())
            {
                var itemKey = systemKey.OpenSubKey(item);
                devices.AddRange(GetDevicesFromKey(itemKey));
            }
            return devices;
        }

        private IEnumerable<IRegistryDevice> GetDevicesFromKey(IRegistryKey itemKey)
        {
            List<IRegistryDevice> devices = new List<IRegistryDevice>();
            foreach (var device in itemKey.GetSubKeyNames())
            {
                var deviceKey = itemKey.OpenSubKey(device);
                var containerId = deviceKey.GetValue("ContainerID");
                string hardwareId = deviceKey.GetValue("HardwareID");

                devices.Add(new RegistryDevice(itemKey.Name, deviceKey.Name, containerId, hardwareId));
            }
            return devices;
        }

        public void Print(List<IRegistryDevice> devices)
        {
            foreach (IRegistryDevice device in devices)
            {
                device.Print();
            }
        }
    }
}
