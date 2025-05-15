using USBForensics.Interfaces;
using USBForensics.Services;

namespace UsbForensics.Services
{
    public class UsbStorageEnumerationDevice : IPrintable
    {
        public UsbStorageEnumerationDevice(string deviceTypeId, string deviceInstanceId, string containerId, string friendlyName, string hardwareId, string service, string deviceDescription)
        {
            DeviceTypeID = deviceTypeId;
            DeviceInstanceID = deviceInstanceId;
            ContainerID = containerId;
            FriendlyName = friendlyName;
            HardwareID = hardwareId;
            Service = service;
            DeviceDescription = deviceDescription;
            VID = UsbDeviceHelpers.ExtractVIDFromHardwareID(hardwareId);
            PID = UsbDeviceHelpers.ExtractPIDFromHardwareID(hardwareId);
            REV = UsbDeviceHelpers.ExtractREVFromHardwareID(hardwareId);
            MI = UsbDeviceHelpers.ExtractMIFromHardwareID(hardwareId);
        }
        public string Get(string key)
        {
            switch (key)
            {
                case "DeviceTypeID":
                    return DeviceTypeID;
                case "DeviceInstanceID":
                    return DeviceInstanceID;
                case "ContainerID":
                    return ContainerID;
                case "HardwareID":
                    return HardwareID;
                default:
                    throw new ArgumentException($"Key '{key}' not found.");
            }
        }

        public string DeviceTypeID { get; }
        public string DeviceInstanceID { get; }
        public string ContainerID { get; }
        public string FriendlyName { get; }
        public string HardwareID { get; }
        public string Service { get; }
        public string DeviceDescription { get; }
        public string VID { get; }
        public string PID { get; }
        public string REV { get; }
        public string MI { get; }

        public void Print()
        {
            Console.WriteLine($"Device Type ID: {DeviceTypeID}");
            Console.WriteLine($"Device Instance ID: {DeviceInstanceID}");
            Console.WriteLine($"Container ID: {ContainerID}");
            Console.WriteLine($"Friendly Name: {FriendlyName}");
            Console.WriteLine($"Hardware ID: {HardwareID}");
            Console.WriteLine($"Service: {Service}");
            Console.WriteLine($"Device Description: {DeviceDescription}");
            Console.WriteLine($"VID: {VID}");
            Console.WriteLine($"PID: {PID}");
            Console.WriteLine($"REV: {REV}");
            Console.WriteLine($"MI: {MI}");
        }
    }
}
