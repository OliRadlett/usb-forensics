using Microsoft.Extensions.DependencyInjection;
using System.Runtime.Versioning;
using UsbForensics.Interfaces;
using UsbForensics.Scanners;
using UsbForensics.Services;

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
            services.AddSingleton<RegistryToJson>();
        }

        private static void Scan(ServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var usbEnumerationScanner = provider.GetRequiredService<UsbEnumerationScanner>();
            var usbStorageEnumerationScanner = provider.GetRequiredService<UsbStorageEnumerationScanner>();

            var usbEnumerationResults = usbEnumerationScanner.Scan();
            var usbStorageEnumerationResults = usbStorageEnumerationScanner.Scan();

            usbEnumerationScanner.Print(usbEnumerationResults);
            usbStorageEnumerationScanner.Print(usbStorageEnumerationResults);

        }

        private static void ExtractRegistry(ServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var json = provider.GetRequiredService<RegistryToJson>();

            json.ExtractRegistry(@"System\CurrentControlSet\Enum\USB", "registry.json");
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