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
            services.AddSingleton<UsbEnumerationScanner>();
        }

        private static void Scan(ServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var scanner = provider.GetRequiredService<UsbEnumerationScanner>();

            var results = scanner.Scan();
            scanner.Print(results);
        }

        private static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            Scan(services);

        }
    }
}