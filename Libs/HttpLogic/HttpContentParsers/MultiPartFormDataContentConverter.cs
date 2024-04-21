using System.Net.Http.Headers;

namespace HttpLogic.HttpContentParsers;

public class MultiPartFormDataContentConverter : IHttpContentConverter
{
    public MediaTypeHeaderValue MediaType => new MediaTypeHeaderValue("multipart/form-data");

    public HttpContent ConvertToHttpContent(object value)
    {
        if (!(value is MultipartFormDataContent formDataContent))
        {
            throw new ArgumentException("Content is not MultipartFormDataContent", nameof(value));
        }

        return formDataContent;
    }

    public Task<TOutput?> ConvertFromHttpContent<TOutput>(HttpContent httpContent)
    {
        throw new NotSupportedException("Conversion from MultiPartFormDataContent is not supported");
    }
}