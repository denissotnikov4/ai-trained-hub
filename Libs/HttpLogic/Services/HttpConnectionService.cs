using HttpLogic.Interfaces;

namespace HttpLogic.Services;

/// <summary>
/// Сервис для создания и управления HTTP-клиентами и отправки HTTP-запросов
/// </summary>
internal class HttpConnectionService : IHttpConnectionService
{
    private readonly IHttpClientFactory _clientFactory;

    public HttpConnectionService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    /// <inheritdoc cref="IHttpConnectionService.CreateHttpClient"/>
    public HttpClient CreateHttpClient(string? clientName = null, TimeSpan? timeOut = null)
    {
        var client = string.IsNullOrWhiteSpace(clientName)
            ? _clientFactory.CreateClient()
            : _clientFactory.CreateClient(clientName);

        if (timeOut != null)
            client.Timeout = timeOut.Value;

        return client;
    }

    /// <inheritdoc cref="IHttpConnectionService.SendRequestAsync"/>
    public async Task<HttpResponseMessage> SendRequestAsync(
        HttpClient httpClient,
        HttpRequestMessage httpRequestMessage,
        HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead,
        CancellationToken cancellationToken = default)
    {
        return await httpClient.SendAsync(httpRequestMessage, httpCompletionOption, cancellationToken);
    }
}