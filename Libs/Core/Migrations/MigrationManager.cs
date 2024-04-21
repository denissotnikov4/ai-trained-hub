using FluentMigrator;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Core.Migration;

/// <summary>
/// Менеджер миграций
/// </summary>
public static class MigrationManager
{
    /// <summary>
    /// Расширение позволяет накатывать миграции
    /// </summary>
    /// <param name="host"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
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