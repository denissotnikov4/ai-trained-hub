using HttpLogic.Models;

namespace HttpLogic.Extensions;

/// <summary>
/// Класс расширений для типа ContentType, предоставляющий дополнительные методы для работы с типами содержимого
/// </summary>
public static class ContentTypeExtensions
{
    /// <summary>
    /// Словарь, сопоставляющий Content Type'ы с их строковыми представлениями
    /// </summary>
    private static readonly Dictionary<ContentType, string> ContentTypeStrings = new()
    {
        { ContentType.Unknown, "unknown" },
        { ContentType.ApplicationJson, "application/json" },
        { ContentType.XWwwFormUrlEncoded, "application/x-www-form-urlencoded" },
        { ContentType.Binary, "binary" },
        { ContentType.ApplicationXml, "application/xml" },
        { ContentType.MultipartFormData, "multipart/form-data" },
        { ContentType.TextXml, "text/xml" },
        { ContentType.TextPlain, "text/plain" },
        { ContentType.ApplicationJwt, "application/jwt" },
        { ContentType.ApplicationOctetStream, "application/octet-stream" }
    };

    /// <summary>
    /// Получает строковое представление Content Type'а
    /// </summary>
    /// <param name="contentType">Тип содержимого</param>
    /// <returns>Строковое представление Content Type'а</returns>
    public static string ToStringRepresentation(this ContentType contentType)
    {
        return ContentTypeStrings[contentType];
    }
}