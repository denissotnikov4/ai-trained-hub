using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HttpLogic.HttpContentParsers;

public class TextPlainContentConverter : IHttpContentConverter
{
    public MediaTypeHeaderValue MediaType => new("text/plain");

    public HttpContent ConvertToHttpContent(object value)
    {
        return new StringContent(value.ToString()!, Encoding.UTF8, MediaType);
    }

    public async Task<TOutput?> ConvertFromHttpContent<TOutput>(HttpContent httpContent)
    {
        return (TOutput)(object)await httpContent.ReadAsStringAsync();
    }
}