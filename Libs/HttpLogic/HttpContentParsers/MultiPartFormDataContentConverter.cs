using System.Net.Http.Headers;
using HttpLogic.Extensions;
using HttpLogic.HttpContentParsers.Interfaces;
using HttpLogic.Models;

namespace HttpLogic.HttpContentParsers;

/// <summary>
/// Реализация конвертера для преобразования объектов в HttpContent и обратно, специализированная для работы с содержимым типа multipart/form-data.
/// </summary>
public class MultiPartFormDataContentConverter : IHttpContentConverter
{
    /// <inheritdoc cref="IHttpContentConverter.MediaType"/>
    public MediaTypeHeaderValue MediaType => new(ContentType.MultipartFormData.ToStringRepresentation());

    /// <inheritdoc cref="IHttpContentConverter.ConvertToHttpContent"/>
    public HttpContent ConvertToHttpContent(object value)
    {
        if (value is not MultipartFormDataContent formDataContent)
        {
            throw new ArgumentException("Content is not MultipartFormDataContent", nameof(value));
        }

        return formDataContent;
    }

    /// <inheritdoc cref="IHttpContentConverter.ConvertFromHttpContent"/>
    public Task<TOutput?> ConvertFromHttpContent<TOutput>(HttpContent httpContent)
    {
        throw new NotSupportedException("Conversion from MultiPartFormDataContent is not supported");
    }
}
