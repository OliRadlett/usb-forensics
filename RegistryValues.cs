using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work_Mentoring_Project
{
    internal class RegistryValues: IRegistryValues
    {
        public RegistryValues(string deviceTypeId, string deviceInstanceId, string hardwareId)
        {
            DeviceTypeID = deviceTypeId;
            DeviceInstanceID = deviceInstanceId;
            HardwareID = hardwareId;
        }

        public string DeviceTypeID{ get; }
        public string DeviceInstanceID{ get; }
        public string HardwareID { get; }

        public void Print()
        {
            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine($"DeviceTypeID: {DeviceTypeID}");
            Console.WriteLine($"DeviceInstanceID: {DeviceInstanceID}");
            Console.WriteLine($"HardwareID: {HardwareID}");
        }

    }
}
