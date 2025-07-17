using FluentAssertions;
using UsbForensics.Interfaces;
using UsbForensics.Scanners;
using Xunit.Abstractions;

namespace UsbForensicsTests
{
    public class Test_UsbStorageScanner
    {
        private readonly ITestOutputHelper output;

        public Test_UsbStorageScanner(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [FileData(typeof(JsonRegistryFileReader), "Resources/registry_usb_storage.json", "Resources/usb_storage_values.json")]
        public void Scan_AddsExpectedProperties(IRegistryRoot registry, List<KeyValuePair<string, string>> values)
        {
            var usbscanner = new UsbStorageEnumerationScanner(registry);

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
                output.WriteLine("Values {0} {1}", expectedValues.Key, expectedValues.Value);
                output.WriteLine("Device {0} {1}", expectedValues.Key, device.Get(expectedValues.Key));
                device.Get(expectedValues.Key).Should().Be(expectedValues.Value);
            }
        }
    }
}