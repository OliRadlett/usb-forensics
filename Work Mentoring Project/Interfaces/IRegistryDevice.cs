namespace UsbForensics.Interfaces
{
    public interface IRegistryDevice
    {
        string ID { get; }
        public void Print();
        string Get(string key);
    }
}
