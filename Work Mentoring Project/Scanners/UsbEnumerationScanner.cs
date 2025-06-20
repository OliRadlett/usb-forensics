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

        public List<UsbDevice> Scan()
        {
            var systemKey = root.GetRegistry(@"System\CurrentControlSet\Enum\USB");
            List<UsbDevice> devices = new List<UsbDevice>();

            Console.WriteLine($"Looking for devices in: {systemKey.Name}".PastelBg(Color.White).Pastel(Color.Black));
            foreach (var item in systemKey.GetSubKeyNames())
            {
                var itemKey = systemKey.OpenSubKey(item);
                devices.AddRange(GetDevicesFromKey(itemKey));
            }
            return devices;
        }

        private IEnumerable<UsbDevice> GetDevicesFromKey(IRegistryKey itemKey)
        {
            List<UsbDevice> devices = new List<UsbDevice>();
            foreach (var device in itemKey.GetSubKeyNames())
            {
                var properties = new Dictionary<string, string>();
                var deviceKey = itemKey.OpenSubKey(device);
                var containerId = deviceKey.GetValue("ContainerID");
                var hardwareId = deviceKey.GetValue("HardwareID");
                var service = deviceKey.GetValue("Service");

                devices.Add(new UsbDevice(itemKey.Name, deviceKey.Name, containerId, hardwareId, service));
            }
            return devices;
        }

        public void Print(List<UsbDevice> devices)
        {
            foreach (UsbDevice device in devices)
            {
                device.Print();
            }
        }
    }
}
