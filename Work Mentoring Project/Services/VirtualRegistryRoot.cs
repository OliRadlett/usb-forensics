using USBForensics.Interfaces;

namespace USBForensics.Services
{
    public class VirtualRegistryRoot : IRegistryRoot
    {
        public VirtualRegistryKey HKLM { get; set; }
        public List<VirtualRegistryValue> ExpectedValues { get; set; } = [];

        public IRegistryKey GetRegistry(string key)
        {
            IRegistryKey registryKey = HKLM;
            var parts = key.Split('\\');

            foreach (var folder in parts)
            {
                if (registryKey.GetSubKeyNames().Contains(folder, StringComparer.InvariantCultureIgnoreCase))
                {
                    registryKey = registryKey.OpenSubKey(folder);
                }
            }

            return new VirtualRegistryKey();
        }
    }
}
