using FluentMigrator;

namespace Dal.Migrations;

[Migration(1)]
public class InitialMigration : Migration
{
    
    public override void Up()
    {
        var sql = $"CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\"";
        Execute.Sql(sql);
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}