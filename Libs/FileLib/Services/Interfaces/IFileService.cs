using System;
using System.Threading.Tasks;
using HttpLogic.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileLib.Services.Interfaces;

/// <summary>
/// Сервис для работы с файлами
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Асинхронно получает файл по его уникальному идентификатору.
    /// </summary>
    /// <param name="fileId">Уникальный идентификатор файла.</param>
    /// <returns>Объект <see cref="HttpDownloadedFile"/>, содержащий информацию о загруженном файле.</returns>
    Task<HttpDownloadedFile> GetFileAsync(Guid fileId);
    
    /// <summary>
    /// Асинхронно сохраняет файл, предоставляя его содержимое в виде массива байтов и имя файла с расширением.
    /// </summary>
    /// <param name="fileBytes">Массив байтов, представляющий содержимое файла.</param>
    /// <param name="fileNameWithExtension">Имя файла с расширением.</param>
    /// <returns>Объект <see cref="HttpUploadedFile"/>, содержащий информацию о сохраненном файле.</returns>
    Task<HttpUploadedFile> SaveFileAsync(byte[] fileBytes, string fileNameWithExtension);
}