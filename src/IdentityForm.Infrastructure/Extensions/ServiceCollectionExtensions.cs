using IdentityForm.Application.Interfaces.Infrastructures.Repositories;
using IdentityForm.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IdentityForm.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IRepositoryAsync<,>), typeof(RepositoryAsync<,>))
                .AddScoped(typeof(IRepositoryNonAuditableAsync<,>), typeof(RepositoryNonAuditableAsync<,>))
                .AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }

    }
}