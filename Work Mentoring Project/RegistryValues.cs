using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Work_Mentoring_Project
{
    internal class RegistryValues: IRegistryValues
    {
        public RegistryValues(string deviceTypeId, string deviceInstanceId, string containerId, string hardwareId)
        {
            DeviceTypeID = deviceTypeId;
            DeviceInstanceID = deviceInstanceId;
            ContainerID = containerId;
            HardwareID = hardwareId;
            (VID, PID, RevID) = ExtractFromHardwareID(hardwareId);
        }

        public string DeviceTypeID { get; }
        public string DeviceInstanceID { get; }
        public string ContainerID { get; }
        public string HardwareID { get; }
        public string VID { get; }
        public string PID { get; }
        public string RevID { get; }


        public void Print()
        {
            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine($"DeviceTypeID: {DeviceTypeID}");
            Console.WriteLine($"DeviceInstanceID: {DeviceInstanceID}");
            Console.WriteLine($"ContainerID: {ContainerID}");
            Console.WriteLine($"HardwareID: {HardwareID}");
            Console.WriteLine($"Version ID: {VID}");
            Console.WriteLine($"PID: {PID}");
            Console.WriteLine($"Revision ID: {RevID}");
        }

        private (string, string, string) ExtractFromHardwareID(string HardwareID)
        {
            string VIDPattern = @"(?<=VID)\w*(?=&)";
            string PIDPattern = @"(?<=PID)\w*(?=&)";
            string RevIDPattern = @"(?<=REV)\w*(?=&)";

            // Add trailing & to make regex easier
            if (!HardwareID.EndsWith("&"))
            {
                HardwareID = HardwareID += "&";
            }

            // Remove all underscores
            HardwareID = HardwareID.Replace("_", "");

            string VID = Regex.Match( HardwareID, VIDPattern ).Value;
            string PID = Regex.Match( HardwareID, PIDPattern ).Value;
            string RevID = Regex.Match( HardwareID, RevIDPattern ).Value;

            return (VID, PID, RevID);
        }

    }
}
