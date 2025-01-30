using Microsoft.Win32;
using Pastel;
using System.Drawing;
using System.Runtime.Versioning;

namespace Work_Mentoring_Project
{
    [SupportedOSPlatform("windows")]
    internal class UsbEnumerationScanner
    {
        public UsbEnumerationScanner()
        {
            List<IRegistryDevice> devices = Scan();
            Print(devices);
        }

        public static List<IRegistryDevice> Scan()
        {
            var key = Registry.LocalMachine;
            var systemKey = key.OpenSubKey(@"System\CurrentControlSet\Enum\USB");

            List<IRegistryDevice> devices = new List<IRegistryDevice>();

            Console.WriteLine($"Looking for devices in: {systemKey}".PastelBg(Color.White).Pastel(Color.Black));
            foreach (var item in systemKey.GetSubKeyNames())
            {
                var itemKey = systemKey.OpenSubKey(item);
                foreach (var device in itemKey.GetSubKeyNames())
                {
                    var deviceKey = itemKey.OpenSubKey(device);
                    var containerId = (string) deviceKey.GetValue("ContainerID");
                    string[] hardwareId = (string[])deviceKey.GetValue("HardwareID");

                    devices.Add(new RegistryDevice(itemKey.Name, deviceKey.Name, containerId, hardwareId[0]));
                }
            }
            return devices;
        }

        public static void Print(List<IRegistryDevice> devices)
        {
            foreach (RegistryDevice device in devices)
            {
                device.Print();
            }
        }
    }
}
