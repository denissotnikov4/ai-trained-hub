using HttpLogic.Interfaces;
using HttpLogic.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HttpLogic.Extensions;

/// <summary>
/// Расширения для класса IServiceCollection, предоставляющие методы для регистрации сервисов, связанных с HTTP-логикой.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрирует сервисы, необходимые для работы с HTTP-запросами и соединениями.
    /// </summary>
    public static IServiceCollection AddHttpLogic(this IServiceCollection collection)
    {
        collection.AddHttpClient();

        collection.AddTransient<IHttpConnectionService, HttpConnectionService>();
        collection.AddTransient<IHttpRequestService, HttpRequestService>();

        return collection;
    }
}