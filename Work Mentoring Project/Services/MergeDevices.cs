using UsbForensics.Interfaces;
using UsbForensics.Services;

namespace USBForensics.Services
{
    public class MergeDevices
    {
        public static List<UsbDevice> Merge(List<UsbEnumerationDevice> usbEnumerationDevices, List<UsbStorageEnumerationDevice> usbStorageEnumerationDevices)
        {
            List<UsbDevice> devices = new List<UsbDevice>();
            foreach (UsbEnumerationDevice usbEnumerationDevice in usbEnumerationDevices)
            {
                foreach (UsbStorageEnumerationDevice usbStorageEnumerationDevice in usbStorageEnumerationDevices)
                {
                    if (usbEnumerationDevice.DeviceInstanceID == usbStorageEnumerationDevice.DeviceInstanceID)
                    {
                        devices.Add(new UsbDevice(usbEnumerationDevice.DeviceInstanceID, usbEnumerationDevice.VID, usbEnumerationDevice.PID, usbEnumerationDevice.REV, usbEnumerationDevice.MI, usbStorageEnumerationDevice.Service));
                        break;
                    }
                }
            }
            return devices;
        }
    }
}
