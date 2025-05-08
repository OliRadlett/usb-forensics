using Pastel;
using System.Drawing;
using UsbForensics.Interfaces;
using UsbForensics.Services;

namespace UsbForensics.Scanners
{
    public class UsbStorageScanner
    {
        private readonly IRegistryRoot root;

        public UsbStorageScanner(IRegistryRoot registryRoot)
        {
            root = registryRoot;
        }

        public List<IRegistryDevice> Scan()
        {
            var systemKey = root.GetRegistry(@"System\CurrentControlSet\Enum\USBSTOR");
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
                var properties = new Dictionary<string, string>();
                var deviceKey = itemKey.OpenSubKey(device);
                var containerId = deviceKey.GetValue("ContainerID");
                var hardwareId = deviceKey.GetValue("HardwareID");
                var id = "propety and" + "another property";

                properties.Add("DeviceTypeID", itemKey.Name);
                properties.Add("DeviceInstanceID", deviceKey.Name);
                // And the rest of the properties

                devices.Add(new RegistryDevice(id, properties));
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
