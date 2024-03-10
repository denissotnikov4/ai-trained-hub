using Dal.Repositories.FileRepository;
using Dal.Repositories.FileRepository.Interfaces;
using Dapper;
using Microsoft.Extensions.DependencyInjection;

namespace Dal;

public static class ForStartup
{
    /// <summary>
    /// Добавление Dal-сервисов к IServiceCollection
    /// </summary>
    public static IServiceCollection AddDalService(this IServiceCollection services)
    {
        services.AddScoped<IFileRepository, FileRepository>();
        return services;
    }
}