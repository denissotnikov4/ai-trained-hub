using System.Net.Http.Headers;
using HttpLogic.Extensions;
using HttpLogic.HttpContentParsers.Interfaces;
using HttpLogic.Models;

namespace HttpLogic.HttpContentParsers;

/// <summary>
/// Реализация конвертера для преобразования объектов в HttpContent и обратно, специализированная для работы с содержимым типа application/octet-stream.
/// </summary>
public class OctetStreamContentConverter : IHttpContentConverter
{
    /// <inheritdoc cref="IHttpContentConverter.MediaType"/>
    public MediaTypeHeaderValue MediaType => new(ContentType.ApplicationOctetStream.ToStringRepresentation());

    /// <inheritdoc cref="IHttpContentConverter.ConvertToHttpContent"/>
    public HttpContent ConvertToHttpContent(object value)
    {
        if (value is not byte[] bytes)
        {
            throw new ArgumentException("Content is not byte[]", nameof(value));
        }

        var content = new ByteArrayContent(bytes);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        return content;
    }

    /// <inheritdoc cref="IHttpContentConverter.ConvertFromHttpContent"/>
    public async Task<TOutput?> ConvertFromHttpContent<TOutput>(HttpContent httpContent)
    {
        ArgumentNullException.ThrowIfNull(httpContent);

        if (typeof(TOutput) == typeof(byte[]))
        {
            return (TOutput)(object)await httpContent.ReadAsByteArrayAsync();
        }

        throw new NotSupportedException($"Conversion to type '{typeof(TOutput)}' is not supported");
    }
}
