using System.Text.Json;
using UsbForensics.Interfaces;

namespace UsbForensics.Services
{
    public class RegistryToJson
    {
        private IRegistryRoot root;
        public RegistryToJson(IRegistryRoot root)
        {
            this.root = root;
        }

        public void ExtractRegistry(string registry, string outputLocation)
        {
            var rootKey = root.GetRegistry(registry);
            var json = AddKey(rootKey);
            using var file = File.OpenWrite(outputLocation);
            JsonSerializer.Serialize(file, json);

        }

        private VirtualRegistryKey AddKey(IRegistryKey key)
        {
            var json = new VirtualRegistryKey();
            json.Name = key.Name;

            foreach (var value in key.GetValueNames())
            {
                var values = new VirtualRegistryValue();
                values.Name = value;
                values.Value = key.GetValue(value);
                json.Values.Add(values);
            }

            foreach (var subKeyName in key.GetSubKeyNames())
            {
                var subKey = key.OpenSubKey(subKeyName);
                if (subKey != null)
                {
                    json.SubKeys.Add(AddKey(subKey));
                }
            }

            return json;
        }
    }
}
