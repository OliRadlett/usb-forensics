namespace UsbForensics.Interfaces
{
    public interface IRegistryDevice
    {
        string DeviceTypeID { get; }
        string DeviceInstanceID { get; }
        string HardwareID { get; }
        public void Print();
        string Get(string key);
    }
}
