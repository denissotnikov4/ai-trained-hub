using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HttpLogic.Extensions;
using HttpLogic.Interfaces;
using HttpLogic.Models;
using Microsoft.AspNetCore.WebUtilities;
using Polly;
using ContentType = HttpLogic.Models.ContentType;

namespace HttpLogic.Services;

/// <summary>
/// Сервис для отправки HTTP-запросов с поддержкой различных типов содержимого и политик устойчивости
/// </summary>
internal class HttpRequestService : IHttpRequestService
{
    private static readonly IDictionary<string, ContentType> ContentTypes = new Dictionary<string, ContentType>
    {
        ["application/json"] = ContentType.ApplicationJson,
        ["application/x-www-form-urlencoded"] = ContentType.XWwwFormUrlEncoded,
        ["application/xml"] = ContentType.ApplicationXml,
        ["text/xml"] = ContentType.TextXml,
        ["text/plain"] = ContentType.TextPlain,
        ["application/jwt"] = ContentType.ApplicationJwt,
        ["multipart/form-data"] = ContentType.MultipartFormData
    };
    
    private readonly IHttpConnectionService _connectionService;

    public HttpRequestService(IHttpConnectionService connectionService)
    {
        _connectionService = connectionService;
    }

    /// <inheritdoc cref="IHttpRequestService.SendRequestAsync"/>
    public async Task<HttpResponseData<TResponse>> SendRequestAsync<TResponse>(
        HttpRequestData requestData,
        HttpConnectionData connectionData = default,
        IAsyncPolicy<HttpResponseMessage>? resiliencePolicy = null,
        CancellationToken cancellationToken = default) where TResponse : class
    {
        var client = _connectionService.CreateHttpClient(connectionData.ClientName, connectionData.TimeOut);
        var httpRequestMessage = CreateHttpRequestMessage(requestData);

        resiliencePolicy ??= Policy.NoOpAsync<HttpResponseMessage>();

        var responseMessage = await resiliencePolicy.ExecuteAsync(async () =>
            await _connectionService
                .SendRequestAsync(
                    client,
                    await httpRequestMessage.ShallowCloneAsync(),
                    cancellationToken: cancellationToken));

        var bodyContent = await GetBodyOfTypeAsync<TResponse>(responseMessage);

        return new HttpResponseData<TResponse>
        {
            Body = bodyContent,
            Headers = responseMessage.Headers,
            StatusCode = responseMessage.StatusCode,
            ContentHeaders = responseMessage.Content.Headers
        };
    }

    /// <summary>
    /// Извлекает тело ответа из сообщения HTTP и преобразует его в указанный тип TResponse.
    /// </summary>
    /// <typeparam name="TResponse">Тип данных, в который должно быть преобразовано тело ответа.</typeparam>
    /// <param name="responseMessage">Сообщение HTTP-ответа, содержащее тело ответа.</param>
    /// <returns>Объект типа TResponse, представляющий тело ответа, или null, если тело ответа отсутствует или статус ответа не указывает на успешное выполнение.</returns>
    private static async Task<TResponse?> GetBodyOfTypeAsync<TResponse>(HttpResponseMessage responseMessage)
        where TResponse : class
    {
        var contentType = responseMessage.Content.Headers.ContentType;

        if (contentType == null || !responseMessage.IsSuccessStatusCode)
            return null;

        if (!ContentTypes.TryGetValue(contentType.MediaType!, out var foundType))
            throw new NotSupportedException($"{contentType.MediaType!} ContentType not supported!");

        return await HttpContentConverterFactory
            .CreateConverter(foundType)
            .ConvertFromHttpContent<TResponse>(responseMessage.Content);
    }

    /// <summary>
    /// Создает объект HttpRequestMessage на основе предоставленных данных запроса.
    /// </summary>
    /// <param name="requestData">Данные HTTP-запроса, включая метод, URI, тело запроса, тип содержимого, заголовки и параметры запроса.</param>
    /// <returns>Объект HttpRequestMessage, готовый к отправке.</returns>
    private static HttpRequestMessage CreateHttpRequestMessage(HttpRequestData requestData)
    {
        var httpConverter = HttpContentConverterFactory.CreateConverter(requestData.ContentType);
        var requestUri = new Uri(
            QueryHelpers.AddQueryString(requestData.Uri.AbsoluteUri, requestData.QueryDictionary));

        var httpRequestMessage = new HttpRequestMessage
        {
            Method = requestData.Method,
            RequestUri = requestUri,
            Content = httpConverter.ConvertToHttpContent(requestData.Body)
        };

        foreach (var headerPair in requestData.HeaderDictionary)
            httpRequestMessage.Headers.Add(headerPair.Key, headerPair.Value);

        return httpRequestMessage;
    }

}