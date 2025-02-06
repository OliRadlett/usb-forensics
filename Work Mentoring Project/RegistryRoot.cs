using Microsoft.Win32;
using System.Runtime.Versioning;

namespace Work_Mentoring_Project
{
    internal class RegistryRoot : IRegistryRoot
    {
        [SupportedOSPlatform("Windows")]
        public IRegistryKey GetRegistry(string key)
        {
            var root = Registry.LocalMachine;
            var registryKey = root;

            var parts = key.Split('\\');

            foreach (var folder in parts)
            {
                if (registryKey.GetSubKeyNames().Contains(folder))
                {
                    registryKey = registryKey.OpenSubKey(folder);
                }
            }

            return new RegistryKey(registryKey);
        }
    }
}
