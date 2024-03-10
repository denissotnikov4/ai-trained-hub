using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Core.Dapper.Connection;

/// <summary>
/// Контекст для работы с Dapper
/// </summary>
public class DapperContext
{
    private readonly string _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Не указан ConnectionString");
    }

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}