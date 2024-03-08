using Dal.Models;
using FluentMigrator;

namespace Dal.Migrations;

[Migration(202402272241)]
public class Date202402272241_AddTable_FileDal : Migration
{
    private readonly string _fileTableName = nameof(FileDal).ToLower();
    
    public override void Up()
    {
        if (!Schema.Table(_fileTableName).Exists())
        {
            Create.Table(_fileTableName)
                .WithColumn(nameof(FileDal.Id)).AsGuid().PrimaryKey().NotNullable().WithDefault(SystemMethods.NewGuid)
                .WithColumn(nameof(FileDal.OriginalFileName)).AsString(250).NotNullable()
                .WithColumn(nameof(FileDal.HandledFileName)).AsString(250).NotNullable()
                .WithColumn(nameof(FileDal.FileExtension)).AsString(20).NotNullable()
                .WithColumn(nameof(FileDal.CreatedTime)).AsDateTime2().NotNullable()
                .WithColumn(nameof(FileDal.FileSize)).AsInt32().NotNullable()
                .WithColumn(nameof(FileDal.AccessModifier)).AsInt16().NotNullable();
        }
    }

    public override void Down()
    {
        Delete.Table(_fileTableName);
    }
}