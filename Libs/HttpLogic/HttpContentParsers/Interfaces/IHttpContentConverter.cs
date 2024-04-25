using System.Net.Http.Headers;

namespace HttpLogic.HttpContentParsers.Interfaces;

/// <summary>
/// Интерфейс для конвертера, предоставляющего функциональность для преобразования объектов в HttpContent и обратно.
/// </summary>
public interface IHttpContentConverter
{
    /// <summary>
    /// MediaType, который поддерживается конвертером.
    /// </summary>
    MediaTypeHeaderValue MediaType { get; }

    /// <summary>
    /// Преобразует объект в HttpContent.
    /// </summary>
    /// <param name="value">Объект для преобразования.</param>
    /// <returns>HttpContent, представляющий преобразованный объект.</returns>
    HttpContent ConvertToHttpContent(object value);

    /// <summary>
    /// Асинхронно преобразует HttpContent обратно в объект указанного типа.
    /// </summary>
    /// <typeparam name="TOutput">Тип объекта, в который следует преобразовать HttpContent.</typeparam>
    /// <param name="httpContent">HttpContent для преобразования.</param>
    /// <returns>Объект типа TOutput, полученный из HttpContent, или null, если преобразование невозможно.</returns>
    Task<TOutput?> ConvertFromHttpContent<TOutput>(HttpContent httpContent);
}