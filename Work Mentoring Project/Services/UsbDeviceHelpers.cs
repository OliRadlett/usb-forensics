using System.Text.RegularExpressions;

namespace USBForensics.Services
{
    public class UsbDeviceHelpers
    {
        public static string ExtractVIDFromHardwareID(string HardwareID)
        {
            string VIDPattern = @"VID(_)?(?<vendorId>[0-9A-F]{4})";
            var match = Regex.Match(HardwareID, VIDPattern);

            if (match.Success)
            {
                return match.Groups["vendorId"].Value;
            }
            return null;
        }

        public static string ExtractPIDFromHardwareID(string HardwareID)
        {
            string PIDPattern = @"PID(_)?(?<productId>[0-9A-F]{4})";
            var match = Regex.Match(HardwareID, PIDPattern);

            if (match.Success)
            {
                return match.Groups["productId"].Value;
            }
            return null;
        }

        public static string ExtractREVFromHardwareID(string HardwareID)
        {
            string REVPattern = @"REV(_)?(?<revision>[0-9A-F]{4})";
            var match = Regex.Match(HardwareID, REVPattern);

            if (match.Success)
            {
                return match.Groups["revision"].Value;
            }
            return null;
        }

        public static string ExtractMIFromHardwareID(string HardwareID)
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
