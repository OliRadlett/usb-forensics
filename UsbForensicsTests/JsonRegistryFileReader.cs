using System.Collections;
using UsbForensics.Services;

namespace UsbForensicsTests
{
    public class JsonRegistryFileReader : IEnumerable<object[]>
    {
        private readonly string _dataPath;
        private readonly string _valuePath;

        public JsonRegistryFileReader(string dataPath, string valuePath)
        {
            _dataPath = dataPath;
            _valuePath = valuePath;
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            var data = System.Text.Json.JsonSerializer.Deserialize<VirtualRegistryRoot>(File.ReadAllText(_dataPath));
            var values = System.Text.Json.JsonSerializer.Deserialize<ExpectedValueFile>(File.ReadAllText(_valuePath));
            foreach (var device in values?.ExpectedValues ?? [])
            {
                var keyValuePairs = device.RegistryKeys?
                    .Select(k => new KeyValuePair<string, string>(k.Name, k.Value))
                    .ToList();
                yield return [data, keyValuePairs];
            }
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ExpectedValueFile
    {
        public List<Device> ExpectedValues { get; set; }

        public class Device
        {
            public List<RegistryKey> RegistryKeys { get; set; }
        }

        public class RegistryKey
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }


}
