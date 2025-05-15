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

        public List<UsbEnumerationDevice> Scan()
        {
            var systemKey = root.GetRegistry(@"System\CurrentControlSet\Enum\USB");
            List<UsbEnumerationDevice> devices = new List<UsbEnumerationDevice>();

            Console.WriteLine($"Looking for devices in: {systemKey.Name}".PastelBg(Color.White).Pastel(Color.Black));
            foreach (var item in systemKey.GetSubKeyNames())
            {
                var itemKey = systemKey.OpenSubKey(item);
                devices.AddRange(GetDevicesFromKey(itemKey));
            }
            return devices;
        }

        private IEnumerable<UsbEnumerationDevice> GetDevicesFromKey(IRegistryKey itemKey)
        {
            List<UsbEnumerationDevice> devices = new List<UsbEnumerationDevice>();
            foreach (var device in itemKey.GetSubKeyNames())
            {
                var properties = new Dictionary<string, string>();
                var deviceKey = itemKey.OpenSubKey(device);
                var containerId = deviceKey.GetValue("ContainerID");
                var hardwareId = deviceKey.GetValue("HardwareID");
                var id = "propety and" + "another property";

                devices.Add(new UsbEnumerationDevice(itemKey.Name, deviceKey.Name, containerId, hardwareId));
            }
            return devices;
        }

        public void Print(List<UsbEnumerationDevice> devices)
        {
            foreach (UsbEnumerationDevice device in devices)
            {
                device.Print();
            }
        }
    }
}
