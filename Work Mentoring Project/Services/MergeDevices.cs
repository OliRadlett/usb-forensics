using UsbForensics.Interfaces;
using UsbForensics.Services;

namespace USBForensics.Services
{
    public class MergeDevices
    {
        public static void Merge(List<UsbDevice> usbDevices, List<UsbStorageDevice> usbStorageDevices)
        {
            foreach (var device in usbDevices.Where(device => device.Service == "USBSTOR"))
            {
                device.StorageDevices = usbStorageDevices
                    .Where(storageDevice => storageDevice.DeviceInstanceID == device.DeviceInstanceID && storageDevice.ContainerID == device.ContainerID)
                    .ToList();
            }
        }
    }
}
