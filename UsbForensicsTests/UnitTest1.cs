using FluentAssertions;
using UsbForensics.Interfaces;
using UsbForensics.Scanners;
using UsbForensics.Services;

namespace UsbForensicsTests
{
    public class UnitTest1
    {
        [Theory]
        [FileData(typeof(JsonRegistryFileReader), "Resources/registry.json", "Resources/values.json")]
        public void Scan_AddsExpectedProperties(IRegistryRoot registry, string key, string? value)
        {
            var usbscanner = new UsbEnumerationScanner(registry);

            var results = usbscanner.Scan();
        }
    }
}