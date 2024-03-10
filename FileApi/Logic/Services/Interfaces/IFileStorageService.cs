using Dal.Models;
using FluentResults;
using Logic.Models;
using Microsoft.AspNetCore.Http;

namespace Logic.Services.Interfaces;

/// <summary>
/// Сервис для хранилища файлов
/// </summary>
public interface IFileStorageService
{
    /// <summary>
    /// Получить содержимое файла в виде массива байт
    /// </summary>
    Task<Stream> GetFileDataAsync(FileModel fileModel);

    /// <summary>
    /// Сохранить файл
    /// </summary>
    Task SaveFileAsync(IFormFile fileData, Guid fileId);

    /// <summary>
    /// Удалить файл
    /// </summary>
    Task DeleteFileAsync(FileModel fileModel);
}