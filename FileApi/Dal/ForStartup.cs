using Dal.Repositories.FileRepository;
using Dal.Repositories.FileRepository.Interfaces;
using Dapper;
using Microsoft.Extensions.DependencyInjection;

namespace Dal;

/// <summary>
/// Класс расширения для IServiceCollection
/// </summary>
public static class ForStartup
{
    /// <summary>
    /// Метод расширения для добавления сервисов доступа к данным (DAL)
    /// </summary>
    public static IServiceCollection AddDalService(this IServiceCollection services)
    {
        services.AddScoped<IFileRepository, FileRepository>();
        return services;
    }
}