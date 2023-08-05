using Amazon.S3;
using IdentityForm.Infrastructure.Shared.Configurations;
using IdentityForm.Infrastructure.Shared.Interfaces;
using IdentityForm.Infrastructure.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace IdentityForm.Infrastructure.Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfraSharedServices(this IServiceCollection services, IConfiguration configuration, IHostEnvironment hostEnvironment = null)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IDateTimeService, DateTimeService>();

            services.AddConfigurations(configuration, hostEnvironment);
            services.AddAwsS3(configuration, hostEnvironment);

            return services;
        }


        public static IServiceCollection AddAwsS3(this IServiceCollection services, IConfiguration configuration, IHostEnvironment hostEnvironment = null)
        {
            services.AddAWSService<IAmazonS3>();
            services.AddSingleton<IS3DataProvider, S3DataProvider>();
            return services;
        }
        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration, IHostEnvironment hostEnvironment = null)
        {
            services.Configure<AwsConfiguration>(sp => configuration.GetSection("AWSConfiguration").Get<AwsConfiguration>());

            return services;
        }
    }
}