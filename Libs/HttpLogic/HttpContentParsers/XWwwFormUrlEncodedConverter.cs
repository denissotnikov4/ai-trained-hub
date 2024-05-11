using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using HttpLogic.Extensions;
using HttpLogic.HttpContentParsers.Interfaces;
using HttpLogic.Models;

namespace HttpLogic.HttpContentParsers;

/// <summary>
/// Реализация конвертера для преобразования объектов в HttpContent и обратно, специализированная для работы с содержимым типа application/x-www-form-urlencoded.
/// </summary>
public class XWwwFormUrlEncodedConverter : IHttpContentConverter
{
    /// <inheritdoc cref="IHttpContentConverter.MediaType"/>
    public MediaTypeHeaderValue MediaType => new(ContentType.XWwwFormUrlEncoded.ToStringRepresentation());

    /// <inheritdoc cref="IHttpContentConverter.ConvertToHttpContent"/>
    public HttpContent ConvertToHttpContent(object value)
    {
        if (value is IEnumerable<KeyValuePair<string, string>> list)
            return new FormUrlEncodedContent(list);

        throw new Exception($"Bad value for {nameof(FormUrlEncodedContent)}");
    }

    /// <inheritdoc cref="IHttpContentConverter.ConvertFromHttpContent"/>
    public async Task<TOutput?> ConvertFromHttpContent<TOutput>(HttpContent httpContent)
    {
        var contentString = await httpContent.ReadAsStringAsync();
        var formData = HttpUtility.ParseQueryString(contentString);

        var result = Activator.CreateInstance<TOutput>();
        var resultType = typeof(TOutput);

        foreach (var key in formData.AllKeys)
        {
            var propertyInfo = resultType.GetProperty(key);

            if (propertyInfo == null || !propertyInfo.CanWrite)
                continue;

            var value = formData[key];
            propertyInfo.SetValue(result, Convert.ChangeType(value, propertyInfo.PropertyType), null);
        }

        return result;
    }
}
