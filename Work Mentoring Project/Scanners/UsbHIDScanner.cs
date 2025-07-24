using Pastel;
using System.Drawing;
using UsbForensics.Interfaces;
using UsbForensics.Services;

namespace UsbForensics.Scanners
{
    public class UsbHIDScanner
    {
        private readonly IRegistryRoot root;

        public UsbHIDScanner(IRegistryRoot registryRoot)
        {
            root = registryRoot;
        }

        public List<UsbHIDDevice> Scan()
        {
            var systemKey = root.GetRegistry(@"System\CurrentControlSet\Enum\HID");
            List<UsbHIDDevice> devices = new List<UsbHIDDevice>();

            Console.WriteLine($"Looking for devices in: {systemKey.Name}".PastelBg(Color.White).Pastel(Color.Black));
            foreach (var item in systemKey.GetSubKeyNames())
            {
                var itemKey = systemKey.OpenSubKey(item);
                devices.AddRange(GetDevicesFromKey(itemKey));
            }
            return devices;
        }

        private IEnumerable<UsbHIDDevice> GetDevicesFromKey(IRegistryKey itemKey)
        {
            List<UsbHIDDevice> devices = new List<UsbHIDDevice>();
            foreach (var device in itemKey.GetSubKeyNames())
            {
                var properties = new Dictionary<string, string>();
                var deviceKey = itemKey.OpenSubKey(device);
                var containerId = deviceKey.GetValue("ContainerID");
                var service = deviceKey.GetValue("Service");

                devices.Add(new UsbHIDDevice(itemKey.Name, deviceKey.Name, containerId, service));
            }
            return devices;
        }

        public void Print(List<UsbHIDDevice> devices)
        {
            foreach (UsbHIDDevice device in devices)
            {
                device.Print();
            }
        }
    }
}
