using System.Reflection;
using Core.Constants;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Migrations.Extensions;

/// <summary>
/// Расширение для работы раннером миграции 
/// </summary>
public static class MigrationRunnerExtension
{
    /// <summary>
    /// Добавляет FluentMigrator в контейнер сервисов для работы с миграциями базы данных.
    /// </summary>
    /// <param name="services">Экземпляр IServiceCollection, который будет настроен для использования FluentMigrator.</param>
    /// <param name="configuration">Экземпляр IConfiguration, содержащий конфигурацию приложения, включая строку подключения к базе данных.</param>
    /// <param name="assembly">Экземпляр Assembly, содержащий миграции, которые должны быть зарегистрированы и выполнены.</param>
    /// <returns>Модифицированный экземпляр IServiceCollection с добавленными сервисами FluentMigrator.</returns>

    public static IServiceCollection AddMigrationRunner(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
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