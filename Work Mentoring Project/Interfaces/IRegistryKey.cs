namespace UsbForensics.Interfaces
{
    public interface IRegistryKey
    {
        string Name { get; }
        string[] GetSubKeyNames();
        string[] GetValueNames();
        IRegistryKey OpenSubKey(string name);
        string GetValue(string valueName);
    }
}
