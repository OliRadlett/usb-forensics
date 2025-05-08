using System.Text.RegularExpressions;
using UsbForensics.Interfaces;

namespace UsbForensics.Services
{
    public class RegistryDevice : IRegistryDevice
    {
        public Dictionary<string, string> properties;
        public string Id;

        public string ID => throw new NotImplementedException();

        public RegistryDevice(string Id, Dictionary<string, string> properties)
        {
            this.Id = Id;
            this.properties = properties;
        }

        public void Print()
        {
            Console.WriteLine("-------------------------------------------------------------------------------------");
            foreach (var property in properties)
            {
                Console.WriteLine($"{property.Key}: {property.Value}");
            }
        }

        public string Get(string key)
        {
            if (properties.ContainsKey(key))
            {
                return properties[key];
            }
            else
            {
                throw new KeyNotFoundException($"Key '{key}' not found in properties.");
            }
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
