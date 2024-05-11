using FileLib.Services;
using FileLib.Services.Interfaces;
using HttpLogic.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace FileLib.Extension;

/// <summary>
/// Расширение для IServiceCollection
/// </summary>
public static class FileServiceCollectionExtension
{
    /// <summary>
    /// Добавляет сервисы, необходимые для работы с файлами, в контейнер зависимостей.
    /// </summary>
    public static IServiceCollection AddFileService(this IServiceCollection services)
    {
        services.AddHttpLogic();
        services.AddSingleton<IFileService, FileService>();
        return services;
    }
}