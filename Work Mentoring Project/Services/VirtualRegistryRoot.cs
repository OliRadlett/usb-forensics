﻿using UsbForensics.Interfaces;

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

            return new VirtualRegistryKey();
        }
    }
}
