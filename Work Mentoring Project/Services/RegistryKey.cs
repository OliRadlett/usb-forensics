using Microsoft.Win32;
using Pastel;
using System.Drawing;
using System.Runtime.Versioning;
using System.Security;
using UsbForensics.Interfaces;

namespace UsbForensics.Services
{
    [SupportedOSPlatform("windows")]
    public class RegistryKey : IRegistryKey
    {

        private Microsoft.Win32.RegistryKey _key;

        public RegistryKey(Microsoft.Win32.RegistryKey key)
        {
            _key = key;
        }

        public string Name { get { return _key.Name; } }

        public string[] GetSubKeyNames()
        {
            return _key.GetSubKeyNames();
        }

        public string[] GetValueNames()
        {
            return _key.GetValueNames();
        }

        public string GetValue(string valueName)
        {
            if (valueName == null)
            {
                return "";
            }

            if (_key.GetValue(valueName) == null)
            {
                return "";
            }

            var kind = _key.GetValueKind(valueName);

            switch (kind)
            {
                case RegistryValueKind.String:
                    return _key.GetValue(valueName).ToString();
                case RegistryValueKind.MultiString:
                    return string.Join(", ", ((string[])_key.GetValue(valueName)!).Select(x => $"{{{x}}}"));
                default:
                    Console.WriteLine($"Warning unsupported RegistryValueKind ({kind})".Pastel(Color.Yellow));
                    return _key.GetValue(valueName).ToString();
            }
        }

        public IRegistryKey OpenSubKey(string name)
        {
            if (_key.GetSubKeyNames().Contains(name))
            {
                try
                {
                    return new RegistryKey(_key.OpenSubKey(name));
                }
                catch (SecurityException)
                {
                    return null;
                }
            }
            return null;
        }
    }
}
