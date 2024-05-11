using Logic.Managers.Detect;
using Logic.Managers.Detect.Interfaces;
using Logic.Managers.Pose;
using Logic.Managers.Pose.Interfaces;
using Logic.Managers.Segment;
using Logic.Managers.Segment.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Logic;

/// <summary>
/// Класс содержит методы расширения для IServiceCollection
/// </summary>
public static class ForStartup
{
    /// <summary>
    /// Добавление Logic-сервисов к IServiceCollection
    /// </summary>
    public static IServiceCollection AddLogicService(this IServiceCollection services)
    {
        services.AddScoped<IDetectManager, DetectManager>();
        services.AddScoped<ISegmentManager, SegmentManager>();
        services.AddScoped<IPoseManager, PoseManager>();

        return services;
    }
}