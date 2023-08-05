using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
namespace IdentityForm.Api.Extensions
{
    internal static class HostBuilderExtensions
    {
        internal static IHostBuilder AddLog(this IHostBuilder builder, IHostEnvironment hostEnvironment)
        {
            var envExtensions = hostEnvironment.EnvironmentName switch
            {
                "Development" => ".Development",
                "Localhost" => ".Localhost",
                _ => "",
            };

            var configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings{envExtensions}.json")
                .AddEnvironmentVariables()
                .Build();        

            return builder;
        }
    }
}