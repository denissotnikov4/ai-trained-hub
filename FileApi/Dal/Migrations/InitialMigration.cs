using FluentMigrator;

namespace Dal.Migrations;

/// <summary>
/// Начальная миграция
/// </summary>
[Migration(1)]
public class InitialMigration : Migration
{
    /// <inheritdoc cref="Migration.Up"/>
    public override void Up()
    {
        var sql = "CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\"";
        Execute.Sql(sql);
    }

    /// <inheritdoc cref="Migration.Down"/>
    public override void Down()
    {
        
    }
}