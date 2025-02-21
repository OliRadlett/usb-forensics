namespace Work_Mentoring_Project
{
    public interface IRegistryDevice
    {
        string DeviceTypeID{ get; }
        string DeviceInstanceID { get; }
        string HardwareID { get; }
        public void Print();
    }
}
