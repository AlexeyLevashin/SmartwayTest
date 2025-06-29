using System.Reflection;
using Domain.Interfaces;
using FluentMigrator.Runner;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddPersistence();
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        // services.AddScoped<IEmployeesRepository, EmployeesRepository>();
        // services.AddScoped<IPassportsRepository, PassportsRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        return services;
    }

    public static IServiceCollection AddMigrations(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddLogging(c => c.AddFluentMigratorConsole())
            .AddFluentMigratorCore()
            .ConfigureRunner(c => c
                .AddPostgres()
                .WithGlobalConnectionString(configuration.GetConnectionString("Database"))
                .ScanIn(Assembly.GetExecutingAssembly()).For.All());
        
        return services;
    }

    public static IServiceProvider UseMigrations(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var runner = scope.ServiceProvider.GetService<IMigrationRunner>();
        runner.MigrateUp();
        
        return serviceProvider;
    }
}