using Dapper;
using Logic.Managers.File.Managers;
using Logic.Managers.File.Managers.Interfaces;
using Logic.Services;
using Logic.Services.Interfaces;
using Logic.Services.Storages;
using Microsoft.Extensions.DependencyInjection;

namespace Logic;

public static class ForStartup
{
    /// <summary>
    /// Добавление Logic-сервисов к IServiceCollection
    /// </summary>
    public static IServiceCollection AddLogicService(this IServiceCollection services)
    {
        services.AddScoped<IFileManager, FileManager>();
        services.AddTransient<IFileStorageService, YandexFileStorageService>();
        
        return services;
    }
}