using UsbForensics.Interfaces;
using USBForensics.Interfaces;
using USBForensics.Services;

namespace UsbForensics.Services
{
    public class UsbDevice : IPrintable, IRegistryDevice
    {
        public UsbDevice(string deviceTypeId, string deviceInstanceId, string containerId, string hardwareId, string service)
        {
            DeviceTypeID = deviceTypeId;
            DeviceInstanceID = deviceInstanceId;
            ContainerID = containerId;
            HardwareID = hardwareId;
            VID = UsbDeviceHelpers.ExtractVIDFromHardwareID(hardwareId);
            PID = UsbDeviceHelpers.ExtractPIDFromHardwareID(hardwareId);
            REV = UsbDeviceHelpers.ExtractREVFromHardwareID(hardwareId);
            MI = UsbDeviceHelpers.ExtractMIFromHardwareID(hardwareId);
            Service = service;
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
                case "Service":
                    return Service;
                default:
                    throw new ArgumentException($"Key '{key}' not found.");
            }
        }

        public string DeviceTypeID { get; }
        public string DeviceInstanceID { get; }
        public string ContainerID { get; }
        public string HardwareID { get; }
        public string VID { get; }
        public string PID { get; }
        public string REV { get; }
        public string MI { get; }
        public string Service { get; }
        public List<UsbStorageDevice> StorageDevices { get; set; }

        public void Print()
        {
            Console.WriteLine($"Device Type ID: {DeviceTypeID}");
            Console.WriteLine($"Device Instance ID: {DeviceInstanceID}");
            Console.WriteLine($"Container ID: {ContainerID}");
            Console.WriteLine($"Hardware ID: {HardwareID}");
            Console.WriteLine($"VID: {VID}");
            Console.WriteLine($"PID: {PID}");
            Console.WriteLine($"REV: {REV}");
            Console.WriteLine($"MI: {MI}");
            Console.WriteLine($"Service: {Service}");

            if (StorageDevices != null && StorageDevices.Count > 0)
            {
                foreach (UsbStorageDevice device in StorageDevices)
                {
                    Console.WriteLine($"Friendly Name: {device.FriendlyName}");
                }
            }
        }
    }
}
