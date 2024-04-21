using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Core.Constants;

namespace Core.Migration.Extensions;

/// <summary>
/// Расширение для работы раннером миграции 
/// </summary>
public static class MigrationRunnerExtension
{
    public static IServiceCollection AddMigrationRunner(this IServiceCollection services, IConfiguration configuration,Assembly assembly)
    {
        services.AddFluentMigratorCore()
            .ConfigureRunner(config =>
                config
                    .AddPostgres()
                    .WithGlobalConnectionString(configuration.GetConnectionString(DataBaseConstants.ConnectionName))
                    .ScanIn(assembly).For.Migrations())
            .AddLogging(config => config.AddFluentMigratorConsole());
        
        return services;
    }
}