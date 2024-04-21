using System.Net.Http.Headers;

namespace HttpLogic.HttpContentParsers;

public class OctetStreamContentConverter : IHttpContentConverter
{
    public MediaTypeHeaderValue MediaType => new MediaTypeHeaderValue("application/octet-stream");

    public HttpContent ConvertToHttpContent(object value)
    {
        if (!(value is byte[] bytes))
        {
            throw new ArgumentException("Content is not byte[]", nameof(value));
        }

        var content = new ByteArrayContent(bytes);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        return content;
    }

    public async Task<TOutput?> ConvertFromHttpContent<TOutput>(HttpContent httpContent)
    {
        if (httpContent == null)
        {
            throw new ArgumentNullException(nameof(httpContent));
        }

        if (typeof(TOutput) == typeof(byte[]))
        {
            return (TOutput)(object)await httpContent.ReadAsByteArrayAsync();
        }

        throw new NotSupportedException($"Conversion to type '{typeof(TOutput)}' is not supported");
    }
}