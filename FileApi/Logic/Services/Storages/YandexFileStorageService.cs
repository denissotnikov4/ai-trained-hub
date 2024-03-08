using AspNetCore.Yandex.ObjectStorage;
using Dal.Helpers;
using Dal.Models;
using FluentResults;
using Logic.Models;
using Logic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Logic.Services;

/// <summary>
/// Хранилище данных в Yandex Cloud Object Storage
/// </summary>
public class YandexFileStorageService : IFileStorageService
{
    private IServiceProvider _serviceProvider;
    
    public YandexFileStorageService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    /// <inheritdoc cref="IFileStorageService.GetFileDataAsync"/>
    public async Task<Stream> GetFileDataAsync(FileModel fileModel)
    {
        var fileStream = new Result<Stream>();
        
        var objectStorageService = _serviceProvider.GetRequiredService<IYandexStorageService>();
        
        var result = await objectStorageService.ObjectService.GetAsync(
            $"{fileModel.FileId}_{fileModel.HandledFileName}.{fileModel.FileExtension}");
        
        if (result.IsSuccessStatusCode)
        {
            fileStream = await result.ReadAsStreamAsync();
        }
        
        return fileStream.Value;
    }

    /// <inheritdoc cref="IFileStorageService.SaveFileAsync"/>
    public async Task SaveFileAsync(IFormFile fileData, Guid fileId)
    {
        var handledUniqueFileName = FileHelper.GetHandledUniqueFileName(fileData.FileName, fileId);

        await using var fileStream = fileData.OpenReadStream();
        var objectStorageService = _serviceProvider.GetRequiredService<IYandexStorageService>();
        await objectStorageService.ObjectService.PutAsync(fileStream, handledUniqueFileName);
    }

    /// <inheritdoc cref="IFileStorageService.DeleteFileAsync"/>
    public async Task DeleteFileAsync(FileModel fileModel)
    {
        var objectStorageService = _serviceProvider.GetRequiredService<IYandexStorageService>();

        var fullFileName = $"{fileModel.FileId}_{fileModel.HandledFileName}.{fileModel.FileExtension}";
        await objectStorageService.ObjectService.DeleteAsync(fullFileName);
    }
}