using Microsoft.Win32;
using Pastel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work_Mentoring_Project
{
    internal class UsbEnumerationScanner
    {
        public UsbEnumerationScanner()
        {
            List<IRegistryValues> devices = Scan();
            foreach (RegistryValues device in devices)
            {
                device.Print();
            }
        }

        public List<IRegistryValues> Scan()
        {
            var key = Registry.LocalMachine;
            var systemKey = key.OpenSubKey(@"System\CurrentControlSet\Enum\USB");

            List<IRegistryValues> devices = new List<IRegistryValues>();

            Console.WriteLine($"Looking for devices in: {systemKey}".PastelBg(Color.White).Pastel(Color.Black));
            foreach (var item in systemKey.GetSubKeyNames())
            {
                var itemKey = systemKey.OpenSubKey(item);
                foreach (var device in itemKey.GetSubKeyNames())
                {
                    var deviceKey = itemKey.OpenSubKey(device);
                    var values = deviceKey.GetValueNames();
                    string[] hardwareId = (string[])deviceKey.GetValue("HardwareID");

                    devices.Add(new RegistryValues(itemKey.Name, deviceKey.Name, hardwareId[0]));
                }
            }
            return devices;
        }
    }
}
