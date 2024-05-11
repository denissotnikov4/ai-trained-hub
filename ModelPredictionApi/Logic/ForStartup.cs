using Logic.Managers.Detect;
using Logic.Managers.Detect.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Logic;

public static class ForStartup
{
    /// <summary>
    /// Добавление Logic-сервисов к IServiceCollection
    /// </summary>
    public static IServiceCollection AddLogicService(this IServiceCollection services)
    {
        services.AddScoped<IDetectManager, DetectManager>();

        return services;
    }
}