namespace UsbForensics.Interfaces
{
    public interface IRegistryRoot
    {
        IRegistryKey GetRegistry(string key);
    }
}
