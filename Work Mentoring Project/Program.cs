using Microsoft.Extensions.DependencyInjection;
using System.Runtime.Versioning;
using Work_Mentoring_Project;

namespace USBForensics
{
    [SupportedOSPlatform("windows")]
    internal class Program
    {
        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<IRegistryRoot, RegistryRoot>();
            services.AddSingleton<UsbEnumerationScanner>();
            services.AddSingleton<RegistryToJson>();
        }

        private static void Scan(ServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var scanner = provider.GetRequiredService<UsbEnumerationScanner>();

            var results = scanner.Scan();
            scanner.Print(results);
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