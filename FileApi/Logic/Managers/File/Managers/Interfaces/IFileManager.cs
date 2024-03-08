using Logic.Records.File.Params;
using Logic.Records.File.Results;
using Microsoft.AspNetCore.Http;

namespace Logic.Managers.File.Managers.Interfaces;

/// <summary>
/// Менеджер для работы с файлом
/// </summary>
public interface IFileManager
{
    /// <summary>
    /// Загрузить файл
    /// </summary>
    Task<CreateFileResult> UploadFileAsync(CreateFileParam createFileParam, IFormFile file);
    
    /// <summary>
    /// Получить файл
    /// </summary>
    Task<GetFileResult> GetFileByIdAsync(Guid fileId);

    /// <summary>
    /// Обновить файл
    /// </summary>
    Task UpdateFileAsync(UpdateFileParam updateFileParam, IFormFile file);
    
    /// <summary>
    /// Удалить файл 
    /// </summary>
    Task DeleteFileAsync(Guid fileId);
}