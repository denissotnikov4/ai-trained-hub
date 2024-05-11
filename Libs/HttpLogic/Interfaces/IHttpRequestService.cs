using HttpLogic.Models;
using Polly;

namespace HttpLogic.Interfaces;

/// <summary>
/// Интерфейс для сервиса, предоставляющего функциональность для отправки HTTP-запросов.
/// </summary>
public interface IHttpRequestService
{
    /// <summary>
    /// Асинхронно отправляет HTTP-запрос и возвращает данные ответа в указанном формате.
    /// </summary>
    /// <typeparam name="TResponse">Тип данных ответа.</typeparam>
    /// <param name="requestData">Данные запроса.</param>
    /// <param name="connectionData">Данные соединения (необязательно).</param>
    /// <param name="resiliencePolicy">Политика устойчивости к ошибкам (необязательно).</param>
    /// <param name="cancellationToken">Токен отмены, который может быть использован для отмены операции.</param>
    /// <returns>Объект с данными ответа и информацией о запросе.</returns>
    Task<HttpResponseData<TResponse>> SendRequestAsync<TResponse>(
        HttpRequestData requestData,
        HttpConnectionData connectionData = default,
        IAsyncPolicy<HttpResponseMessage>? resiliencePolicy = null,
        CancellationToken cancellationToken = default) where TResponse : class;
}