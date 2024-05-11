namespace HttpLogic.Interfaces;

/// <summary>
/// Интерфейс для сервиса, предоставляющего функциональность для работы с HTTP-соединениями.
/// </summary>
public interface IHttpConnectionService
{
    /// <summary>
    /// Создает экземпляр HttpClient с возможностью настройки имени клиента и таймаута.
    /// </summary>
    /// <param name="clientName">Имя клиента, используемое для идентификации HttpClient.</param>
    /// <param name="timeOut">Таймаут для операций HttpClient.</param>
    /// <returns>Экземпляр HttpClient с настроенными параметрами.</returns>
    HttpClient CreateHttpClient(string? clientName = null, TimeSpan? timeOut = null);

    /// <summary>
    /// Асинхронно отправляет HTTP-запрос с использованием предоставленного HttpClient.
    /// </summary>
    /// <param name="httpClient">Используемый HttpClient.</param>
    /// <param name="httpRequestMessage">HTTP-запрос для отправки.</param>
    /// <param name="httpCompletionOption">Опция завершения запроса.</param>
    /// <param name="cancellationToken">Токен отмены, который может быть использован для отмены операции.</param>
    /// <returns>Ответ от сервера.</returns>
    Task<HttpResponseMessage> SendRequestAsync(
        HttpClient httpClient,
        HttpRequestMessage httpRequestMessage,
        HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead,
        CancellationToken cancellationToken = default);
}