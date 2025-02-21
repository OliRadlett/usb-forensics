using Pastel;
using System.Drawing;
using System.Runtime.Versioning;

namespace Work_Mentoring_Project
{
    internal class UsbEnumerationScanner
    {
        private readonly IRegistryRoot root;

        public UsbEnumerationScanner(IRegistryRoot registryRoot)
        {
            root = registryRoot;
        }

        public List<IRegistryDevice> Scan()
        {
            IRegistryRoot root = new RegistryRoot();
            var systemKey = root.GetRegistry(@"System\CurrentControlSet\Enum\USB");

            List<IRegistryDevice> devices = new List<IRegistryDevice>();

            Console.WriteLine($"Looking for devices in: {systemKey}".PastelBg(Color.White).Pastel(Color.Black));
            foreach (var item in systemKey.GetSubKeyNames())
            {
                var itemKey = systemKey.OpenSubKey(item);
                foreach (var device in itemKey.GetSubKeyNames())
                {
                    var deviceKey = itemKey.OpenSubKey(device);
                    var containerId = deviceKey.GetValue("ContainerID");
                    string hardwareId = deviceKey.GetValue("HardwareID");

                    devices.Add(new RegistryDevice(itemKey.Name, deviceKey.Name, containerId, hardwareId));
                }
            }
            return devices;
        }

        public void Print(List<IRegistryDevice> devices)
        {
            foreach (RegistryDevice device in devices)
            {
                device.Print();
            }
        }
    }
}
