using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FileLib.Services.Interfaces;
using HttpLogic.Extensions;
using HttpLogic.Interfaces;
using HttpLogic.Models;
using HttpRequestData = HttpLogic.Models.HttpRequestData;

namespace FileLib.Services;

/// <summary>
/// Сервис файлов
/// </summary>
public class FileService : IFileService
{
    private readonly IHttpRequestService _requestService;

    public FileService(IHttpRequestService requestService)
    {
        _requestService = requestService;
    }
    
    /// <inheritdoc cref="IFileService.GetFileAsync"/>
    public async Task<HttpDownloadedFile> GetFileAsync(Guid fileId)
    {
        var request = new HttpRequestData {
            Method = HttpMethod.Get,
            Uri = new Uri($"http://localhost:5057/file/download-private/?FileId={fileId}")
        };

        var response = await _requestService.SendRequestAsync<HttpDownloadedFile>(request);

        return response.Body;
    }

    /// <inheritdoc cref="IFileService.SaveFileAsync"/>
    public async Task<HttpUploadedFile> SaveFileAsync(byte[] fileBytes, string fileNameWithExtension)
    {
        using var formData = new MultipartFormDataContent();
        using var fileContent = new ByteArrayContent(fileBytes);
        fileContent.Headers.Add("Content-Type", ContentType.ApplicationOctetStream.ToStringRepresentation());
        formData.Add(fileContent, "file", fileNameWithExtension);

        var request = new HttpRequestData
        {
            Method = HttpMethod.Post,
            Uri = new Uri("http://localhost:5057/file/upload"),
            ContentType = ContentType.MultipartFormData,
            Body = formData
        };

        var response = await _requestService.SendRequestAsync<HttpUploadedFile>(request);

        return response.Body;
    }
}