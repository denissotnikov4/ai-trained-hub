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

    /// <summary>
    /// Создает и возвращает новый экземпляр IDbConnection для работы с базой данных PostgreSQL.
    /// </summary>
    /// <returns>Экземпляр IDbConnection, настроенный для работы с PostgreSQL.</returns>
    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}