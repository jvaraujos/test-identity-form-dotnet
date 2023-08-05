using FluentValidation.AspNetCore;
using IdentityForm.Application.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityForm.Api.Extensions
{
    internal static class MvcBuilderExtensions
    {
        [System.Obsolete]
        internal static IMvcBuilder AddValidators(this IMvcBuilder builder)
        {
            builder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AppConfiguration>());
            return builder;
        }
    }
}