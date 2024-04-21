using FileLib.Services;
using FileLib.Services.Interfaces;
using HttpLogic.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace FileLib.Extension;

public static class FileServiceCollectionExtension
{
    public static IServiceCollection AddFileService(this IServiceCollection services)
    {
        services.AddHttpLogic();
        services.AddSingleton<IFileService, FileService>();
        return services;
    }
}