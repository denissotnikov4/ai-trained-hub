using AspNetCore.Yandex.ObjectStorage;
using Core.Exceptions;
using Dal.Helpers;
using FluentResults;
using Logic.Models;
using Logic.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Logic.Services.Storages;

/// <summary>
/// Хранилище данных в Yandex Cloud Object Storage
/// </summary>
public class YandexFileStorageService : IFileStorageService
{
    private readonly IYandexStorageService _objectStorageService;
    
    public YandexFileStorageService(IYandexStorageService objectStorageService)
    {
        _objectStorageService = objectStorageService;
    }
    
    /// <inheritdoc cref="IFileStorageService.GetFileDataStreamAsync"/>
    public async Task<Stream> GetFileDataStreamAsync(FileModel fileModel)
    {
        var fullFileName = $"{fileModel.FileId}_{fileModel.HandledFileName}{fileModel.FileExtension}";
        var response = await _objectStorageService.ObjectService.GetAsync(fullFileName);
        
        var fileStream = new Result<Stream>();
        if (response.IsSuccessStatusCode)
        {
            fileStream = await response.ReadAsStreamAsync();
        }
        
        return fileStream.ValueOrDefault;
    }

    ///  <inheritdoc cref="IFileStorageService.GetFileBytesAsync"/>
    public async Task<byte[]> GetFileBytesAsync(FileModel fileModel)
    {
        var fullFileName = $"{fileModel.FileId}_{fileModel.HandledFileName}{fileModel.FileExtension}";
        var response = await _objectStorageService.ObjectService.GetAsync(fullFileName);
        
        var fileBytesList = new Result<byte[]>();
        if (response.IsSuccessStatusCode)
        {
            fileBytesList = await response.ReadAsByteArrayAsync();
        }
        else
        {
            throw new ObjectStorageException($"Ошибка при получении файла {fileModel.FileId}");
        }

        return fileBytesList.ValueOrDefault;
    }

    /// <inheritdoc cref="IFileStorageService.SaveFileAsync"/>
    public async Task SaveFileAsync(IFormFile fileData, Guid fileId)
    {
        var handledUniqueFileName = FileHelper.GetHandledUniqueFileName(fileData.FileName, fileId);

        await using var fileStream = fileData.OpenReadStream();
        var response = await _objectStorageService.ObjectService.PutAsync(fileStream, handledUniqueFileName);

        if (!response.IsSuccessStatusCode)
        {
            throw new ObjectStorageException($"Ошибка при сохранении файла с идентификатором {fileId} в Object Storage");
        }
    }

    /// <inheritdoc cref="IFileStorageService.DeleteFileAsync"/>
    public async Task DeleteFileAsync(FileModel fileModel)
    {
        var fullFileName = $"{fileModel.FileId}_{fileModel.HandledFileName}{fileModel.FileExtension}";
        var response = await _objectStorageService.ObjectService.DeleteAsync(fullFileName);

        if (!response.IsSuccessStatusCode)
        {
            throw new ObjectStorageException(
                $"Ошибка при удалении файла с идентификатором {fileModel.FileId} из Object Storage");
        }
    }
}