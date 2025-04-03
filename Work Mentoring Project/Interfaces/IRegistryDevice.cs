namespace USBForensics.Interfaces
{
    public interface IRegistryDevice
    {
        string DeviceTypeID { get; }
        string DeviceInstanceID { get; }
        string HardwareID { get; }
        public void Print();
    }
}
