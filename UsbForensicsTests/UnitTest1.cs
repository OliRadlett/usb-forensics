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

        //[Theory]
        //[FileData(typeof(JsonRegistryFileReader), "Resources/registry.json", "Resources/values.json")]
        //public void Scan_AddsExpectedProperties(IRegistryRoot registry)
        //{
        //    var usbscanner = new UsbEnumerationScanner(registry);

        //    var results = usbscanner.Scan();

        //    output.WriteLine("Found {0} devices", results.Count);

        //    foreach (var device in results)
        //    {
        //        output.WriteLine("{0}: {1} should be {2}", key, device.Get(key), value);
        //        device.Get(key).Should().Be(value);
        //    }
        //}
    }
}