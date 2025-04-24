using UsbForensics.Interfaces;

namespace UsbForensics.Services
{
    public class VirtualRegistryRoot : IRegistryRoot
    {
        public VirtualRegistryKey HKLM { get; set; }

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

            return AddKey(registryKey);
        }

        private VirtualRegistryKey AddKey(IRegistryKey key)
        {
            var virtualRegistryKey = new VirtualRegistryKey();
            virtualRegistryKey.Name = key.Name;

            foreach (var value in key.GetValueNames())
            {
                var values = new VirtualRegistryValue();
                values.Name = value;
                values.Value = key.GetValue(value);
                virtualRegistryKey.Values.Add(values);
            }

            foreach (var subKeyName in key.GetSubKeyNames())
            {
                var subKey = key.OpenSubKey(subKeyName);
                if (subKey != null)
                {
                    virtualRegistryKey.SubKeys.Add(AddKey(subKey));
                }
            }

            return virtualRegistryKey;
        }
    }
}
