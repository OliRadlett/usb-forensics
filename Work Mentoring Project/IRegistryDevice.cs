namespace Work_Mentoring_Project
{
    internal interface IRegistryDevice
    {
        string DeviceTypeID{ get; }
        string DeviceInstanceID { get; }
        string HardwareID { get; }
        public void Print();
    }
}
