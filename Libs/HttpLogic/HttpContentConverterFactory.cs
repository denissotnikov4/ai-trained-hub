using System;
using HttpLogic.HttpContentParsers;
using HttpLogic.HttpContentParsers.Interfaces;
using HttpLogic.Models;

namespace HttpLogic;

/// <summary>
/// Статический класс, предоставляющий методы для создания конвертеров содержимого HTTP в зависимости от типа содержимого.
/// </summary>
public static class HttpContentConverterFactory
{
    /// <summary>
    /// Создает конвертер содержимого HTTP на основе указанного типа содержимого.
    /// </summary>
    /// <param name="contentType">Тип содержимого, для которого требуется создать конвертер.</param>
    /// <returns>Конвертер содержимого HTTP, соответствующий указанному типу содержимого.</returns>
    /// <exception cref="NotSupportedException">Выбрасывается, если тип содержимого не поддерживается.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Выбрасывается, если тип содержимого не распознан.</exception>
    public static IHttpContentConverter CreateConverter(ContentType contentType)
    {
        return contentType switch
        {
            ContentType.ApplicationJson => new JsonContentConverter(),
            ContentType.ApplicationXml => new XmlContentConverter(),
            ContentType.TextXml => new XmlContentConverter(),
            ContentType.Binary => new ByteContentConverter(),
            ContentType.XWwwFormUrlEncoded => new XWwwFormUrlEncodedConverter(),
            ContentType.TextPlain => new TextPlainContentConverter(),
            ContentType.MultipartFormData => new MultiPartFormDataContentConverter(),
            ContentType.ApplicationOctetStream => new OctetStreamContentConverter(),
            ContentType.ApplicationJwt => throw new NotSupportedException(),
            ContentType.Unknown => throw new NotSupportedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(contentType), contentType, null)
        };
    }
}
