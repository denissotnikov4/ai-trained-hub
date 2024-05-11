namespace HttpLogic.Models;

/// <summary>
/// Перечисление, представляющее различные типы содержимого, используемые в HTTP-запросах и ответах.
/// </summary>
public enum ContentType
{
    Unknown,
    ApplicationJson,
    XWwwFormUrlEncoded,
    Binary,
    ApplicationXml,
    MultipartFormData,
    TextXml,
    TextPlain,
    ApplicationJwt,
    ApplicationOctetStream
}