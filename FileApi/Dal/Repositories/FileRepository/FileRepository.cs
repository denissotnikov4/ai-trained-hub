using System.Data;
using Core.Constants;
using Dal.Helpers;
using Dal.Models;
using Dal.Repositories.FileRepository.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Dal.Repositories.FileRepository;

/// <inheritdoc cref="IFileRepository"/>
internal class FileRepository : IFileRepository
{
    private static readonly string TableName = $"{nameof(FileDal).ToLower()}";
    private readonly IConfiguration _configuration;

    public FileRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    /// <inheritdoc cref="IFileRepository.SaveFileAsync"/>
    public async Task<Guid> SaveFileAsync(FileDal fileDal)
    {
        var dynamicParameters = FileHelper.GetDynamicParameters(fileDal);

        using IDbConnection db = new NpgsqlConnection(_configuration.GetConnectionString(DataBaseConstants.ConnectionName));
        
        var sql = $"INSERT INTO {TableName} (\"{nameof(fileDal.OriginalFileName)}\", \"{nameof(fileDal.HandledFileName)}\", " +
                  $"\"{nameof(fileDal.FileExtension)}\", \"{nameof(fileDal.CreatedTime)}\", \"{nameof(fileDal.FileSize)}\", " +
                  $"\"{nameof(fileDal.AccessModifier)}\") " +
                  $"VALUES ({DataBaseConstants.ParametersPrefix}{nameof(fileDal.OriginalFileName)}, {DataBaseConstants.ParametersPrefix}{nameof(fileDal.HandledFileName)}, " +
                  $"{DataBaseConstants.ParametersPrefix}{nameof(fileDal.FileExtension)}, {DataBaseConstants.ParametersPrefix}{nameof(fileDal.CreatedTime)}, " +
                  $"{DataBaseConstants.ParametersPrefix}{nameof(fileDal.FileSize)}, {DataBaseConstants.ParametersPrefix}{nameof(fileDal.AccessModifier)}) " +
                  $"RETURNING \"{nameof(fileDal.Id)}\"";
        var fileId = await db.QuerySingleOrDefaultAsync<Guid>(sql, dynamicParameters);

        return fileId;
    }
    
    /// <inheritdoc cref="IFileRepository.GetFileByIdAsync"/>
    public async Task<FileDal> GetFileByIdAsync(Guid fileId)
    {
        var dynamicParameters = new DynamicParameters();
        dynamicParameters.Add(nameof(fileId), fileId);

        using IDbConnection db = new NpgsqlConnection(_configuration.GetConnectionString(DataBaseConstants.ConnectionName));
        
        var sql = $"SELECT * FROM {TableName} " +
                  $"WHERE \"Id\" = {DataBaseConstants.ParametersPrefix}{nameof(fileId)}";
            
        return await db.QuerySingleOrDefaultAsync<FileDal>(sql, dynamicParameters);
    }
    
    /// <inheritdoc cref="IFileRepository.UpdateFileAsync"/>
    public async Task UpdateFileAsync(FileDal fileDal)
    {
        var dynamicParameters = FileHelper.GetDynamicParameters(fileDal);
        var fieldToUpdateList = FileHelper.GetFieldToUpdateList(fileDal);

        using IDbConnection db = new NpgsqlConnection(_configuration.GetConnectionString(DataBaseConstants.ConnectionName));
        
        var sql = $"UPDATE {TableName} " +
                  $"SET {string.Join(", ", fieldToUpdateList)} " +
                  $"WHERE \"{nameof(fileDal.Id)}\" = {DataBaseConstants.ParametersPrefix}{nameof(fileDal.Id)}";

        await db.ExecuteAsync(sql, dynamicParameters);
    }

    /// <inheritdoc cref="IFileRepository.DeleteFileAsync"/>
    public async Task DeleteFileAsync(Guid fileId)
    {
        var dynamicParameters = new DynamicParameters();
        dynamicParameters.Add(nameof(fileId), fileId);
        
        using IDbConnection db = new NpgsqlConnection(_configuration.GetConnectionString(DataBaseConstants.ConnectionName));
        
        var sql = $"DELETE FROM {TableName} " +
                  $"WHERE \"Id\" = {DataBaseConstants.ParametersPrefix}{nameof(fileId)}";
        await db.ExecuteAsync(sql, dynamicParameters);
    }
}