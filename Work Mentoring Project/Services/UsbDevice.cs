using System.ComponentModel;
using USBForensics.Interfaces;

namespace USBForensics.Services
{
    public class UsbDevice : IPrintable
    {
        public string deviceInstanceId { get; }
        public string VID { get; }
        public string PID { get; }
        public string REV { get; }
        public string MI { get; }
        public string service {  get; }

        public UsbDevice(string deviceInstanceId, string VID, string PID, string REV, string MI, string service)
        {
            this.deviceInstanceId = deviceInstanceId;
            this.VID = VID;
            this.PID = PID;
            this.REV = REV;
            this.MI = MI;
            this.service = service;
        }

        public void Print()
        {
            Console.WriteLine($"Device Instance ID: {deviceInstanceId}");
            Console.WriteLine($"Service: {service}");
            Console.WriteLine($"VID: {VID}");
            Console.WriteLine($"PID: {PID}");
            Console.WriteLine($"REV: {REV}");
            Console.WriteLine($"MI: {MI}");
        }
    }
}
