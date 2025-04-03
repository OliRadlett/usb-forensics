using System.Text.RegularExpressions;
using USBForensics.Interfaces;

namespace USBForensics.Services
{
    public class RegistryDevice : IRegistryDevice
    {
        public RegistryDevice(string deviceTypeId, string deviceInstanceId, string containerId, string hardwareId)
        {
            DeviceTypeID = deviceTypeId;
            DeviceInstanceID = deviceInstanceId;
            ContainerID = containerId;
            HardwareID = hardwareId;
            VID = ExtractVIDFromHardwareID(hardwareId);
            PID = ExtractPIDFromHardwareID(hardwareId);
            REV = ExtractREVFromHardwareID(hardwareId);
            MI = ExtractMIFromHardwareID(hardwareId);
        }

        public string DeviceTypeID { get; }
        public string DeviceInstanceID { get; }
        public string ContainerID { get; }
        public string HardwareID { get; }
        public string VID { get; }
        public string PID { get; }
        public string REV { get; }
        public string MI { get; }


        public void Print()
        {
            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine($"DeviceTypeID: {DeviceTypeID}");
            Console.WriteLine($"DeviceInstanceID: {DeviceInstanceID}");
            Console.WriteLine($"ContainerID: {ContainerID}");
            Console.WriteLine($"HardwareID: {HardwareID}");
            Console.WriteLine($"Version ID: {VID}");
            Console.WriteLine($"Product ID: {PID}");
            Console.WriteLine($"Revision: {REV}");
            Console.WriteLine($"MI: {MI}");
        }

        private string ExtractVIDFromHardwareID(string HardwareID)
        {
            string VIDPattern = @"VID(_)?(?<vendorId>[0-9A-F]{4})";
            var match = Regex.Match(HardwareID, VIDPattern);

            if (match.Success)
            {
                return match.Groups["vendorId"].Value;
            }
            return null;
        }

        private string ExtractPIDFromHardwareID(string HardwareID)
        {
            string PIDPattern = @"PID(_)?(?<productId>[0-9A-F]{4})";
            var match = Regex.Match(HardwareID, PIDPattern);

            if (match.Success)
            {
                return match.Groups["productId"].Value;
            }
            return null;
        }

        private string ExtractREVFromHardwareID(string HardwareID)
        {
            string REVPattern = @"REV(_)?(?<revision>[0-9A-F]{4})";
            var match = Regex.Match(HardwareID, REVPattern);

            if (match.Success)
            {
                return match.Groups["revision"].Value;
            }
            return null;
        }

        private string ExtractMIFromHardwareID(string HardwareID)
        {
            string MIPattern = @"MI(_)?(?<interfaceId>[0-9A-F]{2})";
            var match = Regex.Match(HardwareID, MIPattern);

            if (match.Success)
            {
                return match.Groups["interfaceId"].Value;
            }
            return null;
        }

    }
}
