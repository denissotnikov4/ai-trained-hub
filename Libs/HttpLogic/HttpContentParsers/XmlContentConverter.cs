using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using HttpLogic.Extensions;
using HttpLogic.HttpContentParsers.Interfaces;
using HttpLogic.Models;

namespace HttpLogic.HttpContentParsers;

/// <summary>
/// Реализация конвертера для преобразования объектов в HttpContent и обратно, специализированная для работы с содержимым типа application/xml.
/// </summary>
public class XmlContentConverter : IHttpContentConverter
{
    /// <inheritdoc cref="IHttpContentConverter.MediaType"/>
    public MediaTypeHeaderValue MediaType => new(ContentType.ApplicationXml.ToStringRepresentation());

    /// <inheritdoc cref="IHttpContentConverter.ConvertToHttpContent"/>
    public HttpContent ConvertToHttpContent(object value)
    {
        var stringBuilder = new StringBuilder();
        var xmlSerializer = new XmlSerializer(value.GetType());

        using var writer = new StringWriter(stringBuilder);
        xmlSerializer.Serialize(writer, value);

        return new StringContent(stringBuilder.ToString(), Encoding.UTF8, MediaType);
    }

    /// <inheritdoc cref="IHttpContentConverter.ConvertFromHttpContent"/>
    public async Task<TOutput?> ConvertFromHttpContent<TOutput>(HttpContent httpContent)
    {
        var stringContent = await httpContent.ReadAsStringAsync();
        var xmlSerializer = new XmlSerializer(typeof(TOutput));

        using var reader = new StringReader(stringContent);
        return (TOutput)xmlSerializer.Deserialize(reader)!;
    }
}
