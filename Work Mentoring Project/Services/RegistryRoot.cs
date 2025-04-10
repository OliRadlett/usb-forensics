using Microsoft.Win32;
using System.Runtime.Versioning;
using UsbForensics.Interfaces;

namespace UsbForensics.Services
{
    public class RegistryRoot : IRegistryRoot
    {
        [SupportedOSPlatform("Windows")]
        public IRegistryKey GetRegistry(string key)
        {
            var root = Registry.LocalMachine;
            var registryKey = root;

            var parts = key.Split('\\');

            foreach (var folder in parts)
            {
                if (registryKey.GetSubKeyNames().Contains(folder, StringComparer.InvariantCultureIgnoreCase))
                {
                    registryKey = registryKey.OpenSubKey(folder);
                }
            }

            return new RegistryKey(registryKey);
        }
    }
}
