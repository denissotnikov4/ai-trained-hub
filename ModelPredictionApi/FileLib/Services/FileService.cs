using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FileLib.Services.Interfaces;
using HttpLogic.Contracts;
using HttpLogic.Models;
using HttpRequestData = HttpLogic.Models.HttpRequestData;

namespace FileLib.Services;

public class FileService : IFileService
{
    private readonly IHttpRequestService _requestService;
    private readonly HttpClient _httpClient;
    
    public FileService(IHttpRequestService requestService, HttpClient httpClient)
    {
        _requestService = requestService;
        _httpClient = httpClient;
    }
    
    public async Task<HttpDownloadedFile> GetFileAsync(Guid fileId)
    {
        var request = new HttpRequestData {
            Method = HttpMethod.Get,
            Uri = new Uri($"http://localhost:5057/file/download-private/?FileId={fileId}")
        };

        var response = await _requestService.SendRequestAsync<HttpDownloadedFile>(
            request
        );

        return response.Body;
    }
    

    public async Task<HttpUploadedFile> SaveFileAsync(byte[] fileBytes, string fileNameWithExtension)
    {
        using var formData = new MultipartFormDataContent();
        using var fileContent = new ByteArrayContent(fileBytes);
        fileContent.Headers.Add("Content-Type", "application/octet-stream");
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
    
    public async Task<Guid> UploadFileAsync(byte[] fileBytes, string fileNameWithExtension)
    {
        using var formData = new MultipartFormDataContent();
        using var fileContent = new ByteArrayContent(fileBytes);
        fileContent.Headers.Add("Content-Type", "application/octet-stream");
        formData.Add(fileContent, "file", fileNameWithExtension);

        var response = await _httpClient.PostAsync("http://localhost:5057/file/upload", formData);
        response.EnsureSuccessStatusCode();

        var fileId = await response.Content.ReadFromJsonAsync<Guid>();
        return fileId;
    }
}