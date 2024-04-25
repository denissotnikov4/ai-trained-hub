using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Core.Migrations;

/// <summary>
/// Менеджер миграций
/// </summary>
public static class MigrationManager
{
    /// <summary>
    /// Расширение позволяет накатывать миграции
    /// </summary>
    public static IHost MigrateDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        try
        {
            migrationService.MigrateUp();
        }
        catch
        {
            throw new Exception("Не получилось накатить миграцию");
        }

        return host;
    }
}