using EntityFramework.Exceptions.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HeroApi.Application.Common.Interfaces;
using HeroApi.Infrastructure.Persistence;
using HeroApi.Infrastructure.Persistence.Interceptors;

namespace HeroApi.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IHostApplicationBuilder builder)
    {
        builder.AddSqlServerDbContext<ApplicationDbContext>("Clean-Architecture",
            null,
            options =>
            {
                var serviceProvider = builder.Services.BuildServiceProvider();
                options.AddInterceptors(
                    serviceProvider.GetRequiredService<EntitySaveChangesInterceptor>(),
                    serviceProvider.GetRequiredService<DispatchDomainEventsInterceptor>());

                // Return strongly typed useful exceptions
                options.UseExceptionProcessor();
            });

        var services = builder.Services;

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<EntitySaveChangesInterceptor>();
        services.AddScoped<DispatchDomainEventsInterceptor>();

        services.AddSingleton(TimeProvider.System);
    }
}