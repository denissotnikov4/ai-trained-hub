using System.Net;
using System.Net.Http.Headers;

namespace HttpLogic.Models;

/// <summary>
/// Базовый класс для представления HTTP-ответа, содержащий информацию о статусе, заголовках и содержимом.
/// </summary>
public record BaseHttpResponse
{
    /// <summary>
    /// Статус-код HTTP-ответа.
    /// </summary>
    public HttpStatusCode StatusCode { get; init; }

    /// <summary>
    /// Заголовки HTTP-ответа.
    /// </summary>
    public HttpResponseHeaders Headers { get; init; } = null!;

    /// <summary>
    /// Заголовки содержимого HTTP-ответа.
    /// </summary>
    public HttpContentHeaders ContentHeaders { get; init; } = null!;

    /// <summary>
    /// Определяет, является ли статус-код успешным (в диапазоне 200-299).
    /// </summary>
    public bool IsSuccessStatusCode => (int)StatusCode is >= 200 and <= 299;
}
