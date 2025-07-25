using Microsoft.Extensions.DependencyInjection;
using System.Runtime.Versioning;
using UsbForensics.Interfaces;
using UsbForensics.Scanners;
using UsbForensics.Services;
using USBForensics.Services;

namespace UsbForensics
{
    [SupportedOSPlatform("windows")]
    internal class Program
    {
        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<IRegistryRoot, RegistryRoot>();
            services.AddSingleton<UsbEnumerationScanner>();
            services.AddSingleton<UsbStorageEnumerationScanner>();
            services.AddSingleton<UsbHIDScanner>();
            services.AddSingleton<RegistryToJson>();
        }

        private static void Scan(ServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var usbEnumerationScanner = provider.GetRequiredService<UsbEnumerationScanner>();
            var usbStorageEnumerationScanner = provider.GetRequiredService<UsbStorageEnumerationScanner>();
            var usbHIDScanner = provider.GetRequiredService<UsbHIDScanner>();

            var usbEnumerationResults = usbEnumerationScanner.Scan();
            var usbStorageEnumerationResults = usbStorageEnumerationScanner.Scan();
            var usbHIDResults = usbHIDScanner.Scan();

            //usbEnumerationScanner.Print(usbEnumerationResults);
            //usbStorageEnumerationScanner.Print(usbStorageEnumerationResults);
            //var mergeDevices = MergeDevices.Merge(usbEnumerationResults, usbStorageEnumerationResults);

            MergeDevices.Merge(usbEnumerationResults, usbStorageEnumerationResults);

            usbEnumerationScanner.Print(usbEnumerationResults);

        }

        private static void ExtractRegistry(ServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var json = provider.GetRequiredService<RegistryToJson>();

            json.ExtractRegistry(@"System\CurrentControlSet\Enum\USBStor", "registry_usb_storage.json");
        }

        private static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            ExtractRegistry(services);
            Scan(services);

        }
    }
}