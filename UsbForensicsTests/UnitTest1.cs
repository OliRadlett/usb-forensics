using FluentAssertions;
using UsbForensics.Interfaces;
using UsbForensics.Scanners;
using Xunit.Abstractions;

namespace UsbForensicsTests
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper output;

        public UnitTest1(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [FileData(typeof(JsonRegistryFileReader), "Resources/registry.json", "Resources/values.json")]
        public void Scan_AddsExpectedProperties(IRegistryRoot registry, List<KeyValuePair<string, string>> values)
        {
            var usbscanner = new UsbEnumerationScanner(registry);

            var results = usbscanner.Scan();

            output.WriteLine("Found {0} devices", results.Count);
            output.WriteLine("Found {0} expected values", values.Count);

            for (var i = 0; i < results.Count; i++)
            {
                if (i >= values.Count)
                {
                    output.WriteLine("No expected value for device {0}", i);
                    continue;
                }
                var device = results[i];
                var expectedValues = values[i];
                output.WriteLine("Registry HWID {0}", device.HardwareID);
                output.WriteLine("Values HWID {0}", expectedValues.Value);
                //output.WriteLine("{0}: {1} should be {2}", key, device.Get(key), value);
                //device.Get(key).Should().Be(value);
                //output.WriteLine(key);
            }
        }
    }
}