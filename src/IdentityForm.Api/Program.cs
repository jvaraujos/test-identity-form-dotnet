using IdentityForm.Api.Extensions;
using IdentityForm.Api.Managers.Preferences;
using IdentityForm.Api.Middlewares;
using IdentityForm.Application.Extensions;
using IdentityForm.Application.Interfaces.Common;
using IdentityForm.Infrastructure.Contexts;
using IdentityForm.Infrastructure.Extensions;
using IdentityForm.Infrastructure.Shared.Configurations;
using IdentityForm.Infrastructure.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
IConfiguration configuration = builder.Configuration;
var webHostEnvironment = builder.Environment;
builder.Services.AddForwarding(configuration);
builder.Services.AddHealthChecks();

builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});
builder.Services.AddInfraSharedServices(configuration);
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddSerialization();
builder.Services.AddDatabase(configuration);
builder.Services.AddScoped<ServerPreferenceManager>();
builder.Services.AddServerLocalization();
builder.Services.AddApplicationLayer();
builder.Services.AddRepositories();
builder.Services.RegisterSwagger();
builder.Services.AddInfrastructureMappings();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddRazorPages();
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
});
builder.Services.Configure<AwsConfiguration>(configuration.GetSection(nameof(AwsConfiguration)));

builder.Services.AddLazyCache();
builder.Services.AddMemoryCache();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Host.AddLog(webHostEnvironment);

var app = builder.Build();
MigrationDatabase(app);

app.MapHealthChecks("healthz");
app.UseForwarding(configuration);
app.UseExceptionHandling(webHostEnvironment);
app.UseHsts();
app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseStaticFiles();
app.UseRequestLocalizationByCulture();
app.UseRouting();
app.ConfigureSwagger();
app.UseEndpoints();
app.Run();
void MigrationDatabase(IApplicationBuilder app)
{
    using (var serviceScope = app.ApplicationServices.CreateScope())
    {
        var context = serviceScope.ServiceProvider.GetService<IdentityFormContext>();
        context.RunMigrate();
    }
}