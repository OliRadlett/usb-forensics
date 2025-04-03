namespace USBForensics.Interfaces
{
    public interface IRegistryRoot
    {
        IRegistryKey GetRegistry(string key);
    }
}
