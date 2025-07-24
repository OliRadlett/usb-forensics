using UsbForensics.Interfaces;
using USBForensics.Interfaces;
using USBForensics.Services;

namespace UsbForensics.Services
{
    public class UsbHIDDevice : IPrintable, IRegistryDevice
    {
        public UsbHIDDevice(string deviceTypeId, string deviceInstanceId, string containerId, string service)
        {
            DeviceTypeID = deviceTypeId;
            DeviceInstanceID = deviceInstanceId;
            ContainerID = containerId;
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
                case "Service":
                    return Service;
                default:
                    throw new ArgumentException($"Key '{key}' not found.");
            }
        }

        public string DeviceTypeID { get; }
        public string DeviceInstanceID { get; }
        public string ContainerID { get; }
        public string Service { get; }

        public void Print()
        {
            Console.WriteLine($"Device Type ID: {DeviceTypeID}");
            Console.WriteLine($"Device Instance ID: {DeviceInstanceID}");
            Console.WriteLine($"Container ID: {ContainerID}");
            Console.WriteLine($"Service: {Service}");
        }
    }
}
