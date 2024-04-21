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
    /// Получить файл
    /// </summary>
    Task<HttpDownloadedFile> GetFileAsync(Guid fileId);
    
    ///  <summary>
    /// Сохранить файл
    /// </summary>
    Task<HttpUploadedFile> SaveFileAsync(byte[] fileBytes, string fileNameWithExtension);

    Task<Guid> UploadFileAsync(byte[] fileBytes, string fileNameWithExtension);

}