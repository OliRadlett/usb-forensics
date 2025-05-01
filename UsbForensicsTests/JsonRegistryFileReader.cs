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
                yield return [data, device.keys];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


    public class RegistryFile
    {
        public List<RegistryEntry> Registry{ get; set; }

        public class RegistryEntry
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }

    public class ExpectedValueFile
    {
        public List<Device> ExpectedValues { get; set; }

        public class Device
        {
            public List<KeyAndValue> keys { get; set; }
        }

        public class KeyAndValue
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }

}
