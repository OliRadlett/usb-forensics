using UsbForensics.Interfaces;

namespace UsbForensics.Services
{
    public class VirtualRegistryKey : IRegistryKey
    {
        public string Name { get; set; }
        public List<VirtualRegistryValue> Values { get; set; } = [];
        public List<VirtualRegistryKey> SubKeys { get; set; } = [];

        public string[] GetSubKeyNames()
        {
            return SubKeys.Select(x => x.Name).ToArray();
        }

        public string[] GetValueNames()
        {
            return Values.Select(x => x.Name).ToArray();
        }

        public string GetValue(string valueName)
        {
            return Values.FirstOrDefault(x => x.Name == valueName).Value;
        }

        public IRegistryKey OpenSubKey(string name)
        {
            return SubKeys.FirstOrDefault(x => x.Name == name);
        }
    }
}
