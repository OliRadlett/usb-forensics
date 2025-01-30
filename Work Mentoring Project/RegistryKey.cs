using System.Runtime.Versioning;

namespace Work_Mentoring_Project
{
    [SupportedOSPlatform("windows")]
    internal class RegistryKey : IRegistryKey
    {

        private Microsoft.Win32.RegistryKey _key;

        public RegistryKey(Microsoft.Win32.RegistryKey key)
        {
            _key = key;
        }

        public string Name {  get { return _key.Name; } }

        public string[] GetSubkeyNames()
        {
            return _key.GetSubKeyNames();
        }

        public string GetValue(string valueName)
        {
            return (string)_key.GetValue(valueName);
        }

        public IRegistryKey OpenSubKey(string name)
        {
            return new RegistryKey(_key.OpenSubKey(name));
        }
    }
}
