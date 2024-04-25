using System;
using System.Collections.Generic;
using System.Net.Http;

namespace HttpLogic.Models;

/// <summary>
/// Запись, содержащая данные HTTP-запроса, включая метод, URI, тело запроса, тип содержимого, заголовки и параметры запроса.
/// </summary>
public record HttpRequestData
{
    /// <summary>
    /// Метод HTTP-запроса
    /// </summary>
    public HttpMethod Method { get; init; } = null!;

    /// <summary>
    /// URI, к которому направлен запрос.
    /// </summary>
    public Uri Uri { get; init; } = null!;

    /// <summary>
    /// Тело запроса, содержащее данные для отправки на сервер.
    /// </summary>
    public object Body { get; init; } = null!;

    /// <summary>
    /// Тип содержимого запроса, указывающий на формат данных в теле запроса.
    /// </summary>
    public ContentType ContentType { get; init; } = ContentType.ApplicationJson;

    /// <summary>
    /// Словарь заголовков запроса, где ключ - это имя заголовка, а значение - соответствующее значение заголовка.
    /// </summary>
    public IDictionary<string, string> HeaderDictionary { get; init; } = new Dictionary<string, string>();

    /// <summary>
    /// Словарь параметров запроса, где ключ - это имя параметра, а значение - соответствующее значение параметра.
    /// </summary>
    public IDictionary<string, string> QueryDictionary { get; init; } = new Dictionary<string, string>();
}
