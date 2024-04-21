using AspNetCore.Yandex.ObjectStorage;
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
        var result = await _objectStorageService.ObjectService.GetAsync(
            $"{fileModel.FileId}_{fileModel.HandledFileName}.{fileModel.FileExtension}");
        
        var fileStream = new Result<Stream>();
        if (result.IsSuccessStatusCode)
        {
            fileStream = await result.ReadAsStreamAsync();
        }
        
        return fileStream.Value;
    }

    ///  <inheritdoc cref="IFileStorageService.GetFileBytesAsync"/>
    public async Task<byte[]> GetFileBytesAsync(FileModel fileModel)
    {
        var result = await _objectStorageService.ObjectService.GetAsync(
            $"{fileModel.FileId}_{fileModel.HandledFileName}.{fileModel.FileExtension}");
        
        var fileStream = new Result<byte[]>();
        if (result.IsSuccessStatusCode)
        {
            fileStream = await result.ReadAsByteArrayAsync();
        }
        
        return fileStream.Value;
    }

    /// <inheritdoc cref="IFileStorageService.SaveFileAsync"/>
    public async Task SaveFileAsync(IFormFile fileData, Guid fileId)
    {
        var handledUniqueFileName = FileHelper.GetHandledUniqueFileName(fileData.FileName, fileId);

        await using var fileStream = fileData.OpenReadStream();
        await _objectStorageService.ObjectService.PutAsync(fileStream, handledUniqueFileName);
    }

    /// <inheritdoc cref="IFileStorageService.DeleteFileAsync"/>
    public async Task DeleteFileAsync(FileModel fileModel)
    {
        var fullFileName = $"{fileModel.FileId}_{fileModel.HandledFileName}.{fileModel.FileExtension}";
        await _objectStorageService.ObjectService.DeleteAsync(fullFileName);
    }
}