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
    /// Получает содержимое файла в виде потока данных.
    /// </summary>
    /// <param name="fileModel">Модель файла, содержащая информацию о файле.</param>
    /// <returns>Поток данных файла.</returns>
    Task<Stream> GetFileDataStreamAsync(FileModel fileModel);

    /// <summary>
    /// Получает содержимое файла в виде массива байтов.
    /// </summary>
    /// <param name="fileModel">Модель файла, содержащая информацию о файле.</param>
    /// <returns>Массив байтов, представляющий содержимое файла.</returns>
    Task<byte[]> GetFileBytesAsync(FileModel fileModel);

    /// <summary>
    /// Сохраняет файл на сервере.
    /// </summary>
    /// <param name="fileData">Объект IFormFile, содержащий данные файла.</param>
    /// <param name="fileId">Уникальный идентификатор файла, используемый для сохранения.</param>
    /// <returns></returns>
    Task SaveFileAsync(IFormFile fileData, Guid fileId);

    /// <summary>
    /// Удаляет файл с сервера.
    /// </summary>
    /// <param name="fileModel">Модель файла, содержащая информацию о файле для удаления.</param>
    /// <returns></returns>
    Task DeleteFileAsync(FileModel fileModel);
}