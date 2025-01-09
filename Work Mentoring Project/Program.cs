using Microsoft.Win32;
using System.Drawing;
using System.Runtime.Versioning;
using Pastel;
using Work_Mentoring_Project;

namespace USBForensics
{
    internal class Program
    {
        [SupportedOSPlatform("windows")]
        static void Main(string[] args)
        {
            var key = Registry.LocalMachine;
            var systemKey = key.OpenSubKey(@"System\CurrentControlSet\Enum\USB");

            List<IRegistryValues> devices = new List<IRegistryValues> ();

            Console.WriteLine($"Looking for devices in: {systemKey}".PastelBg(Color.White).Pastel(Color.Black));
            foreach (var item in systemKey.GetSubKeyNames())
            {
                var itemKey = systemKey.OpenSubKey(item);
                foreach (var device in itemKey.GetSubKeyNames())
                {
                    var deviceKey = itemKey.OpenSubKey(device);
                    var values = deviceKey.GetValueNames();
                    string[] hardwareId = (string[]) deviceKey.GetValue("HardwareID");

                    devices.Add(new RegistryValues(itemKey.Name, deviceKey.Name, hardwareId[0]));
                }
            }

            foreach (RegistryValues device in devices)
            {
                device.Print();
            }
        }
    }
}