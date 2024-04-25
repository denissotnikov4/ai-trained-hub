using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using HttpLogic.Extensions;
using HttpLogic.HttpContentParsers.Interfaces;
using HttpLogic.Models;

namespace HttpLogic.HttpContentParsers;

/// <summary>
/// Реализация конвертера для преобразования объектов в HttpContent и обратно, специализированная для работы с содержимым типа text/plain.
/// </summary>
public class TextPlainContentConverter : IHttpContentConverter
{
    /// <inheritdoc cref="IHttpContentConverter.MediaType"/>
    public MediaTypeHeaderValue MediaType => new(ContentType.TextPlain.ToStringRepresentation());

    /// <inheritdoc cref="IHttpContentConverter.ConvertToHttpContent"/>
    public HttpContent ConvertToHttpContent(object value)
    {
        return new StringContent(value.ToString()!, Encoding.UTF8, MediaType);
    }

    /// <inheritdoc cref="IHttpContentConverter.ConvertFromHttpContent"/>
    public async Task<TOutput?> ConvertFromHttpContent<TOutput>(HttpContent httpContent)
    {
        return (TOutput)(object)await httpContent.ReadAsStringAsync();
    }
}
