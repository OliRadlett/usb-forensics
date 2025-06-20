using Pastel;
using System.Drawing;
using UsbForensics.Interfaces;
using UsbForensics.Services;

namespace UsbForensics.Scanners
{
    public class UsbStorageEnumerationScanner
    {
        private readonly IRegistryRoot root;

        public UsbStorageEnumerationScanner(IRegistryRoot registryRoot)
        {
            root = registryRoot;
        }

        public List<UsbStorageDevice> Scan()
        {
            var systemKey = root.GetRegistry(@"System\CurrentControlSet\Enum\USBStor");
            List<UsbStorageDevice> devices = new List<UsbStorageDevice>();

            Console.WriteLine($"Looking for devices in: {systemKey.Name}".PastelBg(Color.White).Pastel(Color.Black));
            foreach (var item in systemKey.GetSubKeyNames())
            {
                var itemKey = systemKey.OpenSubKey(item);
                devices.AddRange(GetDevicesFromKey(itemKey));
            }
            return devices;
        }

        private IEnumerable<UsbStorageDevice> GetDevicesFromKey(IRegistryKey itemKey)
        {
            List<UsbStorageDevice> devices = new List<UsbStorageDevice>();
            foreach (var device in itemKey.GetSubKeyNames())
            {
                var deviceKey = itemKey.OpenSubKey(device);
                var containerId = deviceKey.GetValue("ContainerID");
                string friendlyName = deviceKey.GetValue("FriendlyName");
                string hardwareId = deviceKey.GetValue("HardwareID");
                string service = deviceKey.GetValue("Service");
                string deviceDescription = deviceKey.GetValue("DeviceDesc");

                devices.Add(new UsbStorageDevice(itemKey.Name, deviceKey.Name, containerId, friendlyName, hardwareId, service, deviceDescription));
            }
            return devices;
        }

        public void Print(List<UsbStorageDevice> devices)
        {
            foreach (UsbStorageDevice device in devices)
            {
                device.Print();
            }
        }
    }
}
