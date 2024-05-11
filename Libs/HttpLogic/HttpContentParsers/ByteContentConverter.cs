using System;
using System.Net.Http;
using System.Threading.Tasks;
using HttpLogic.HttpContentParsers.Interfaces;
using MediaTypeHeaderValue = System.Net.Http.Headers.MediaTypeHeaderValue;

namespace HttpLogic.HttpContentParsers;

/// <summary>
/// Реализация конвертера для преобразования объектов в HttpContent и обратно, специализированная для работы с содержимым типа byte[].
/// </summary>
public class ByteContentConverter : IHttpContentConverter
{
    /// <inheritdoc cref="IHttpContentConverter.MediaType"/>
    public MediaTypeHeaderValue MediaType => new(string.Empty);

    /// <inheritdoc cref="IHttpContentConverter.ConvertToHttpContent"/>
    public HttpContent ConvertToHttpContent(object value)
    {
        if (value.GetType() == typeof(byte[]))
        {
            return new ByteArrayContent((byte[])value);
        }

        throw new Exception($"Bad value for {nameof(ByteArrayContent)}");
    }

    /// <inheritdoc cref="IHttpContentConverter.ConvertFromHttpContent"/>
    public async Task<TOutput?> ConvertFromHttpContent<TOutput>(HttpContent httpContent)
    {
        var byteArray = await httpContent.ReadAsByteArrayAsync();
        return (TOutput)(object)byteArray;
    }
}