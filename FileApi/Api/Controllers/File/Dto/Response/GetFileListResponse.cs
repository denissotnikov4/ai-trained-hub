using Newtonsoft.Json;

namespace Api.Controllers.File.Dto.Response;

/// <summary>
/// Ответ на получение списка файлов
/// </summary>
public record GetFileListResponse
{
    /// <summary>
    /// Список полученных файлов
    /// </summary>
    [JsonProperty("fileList")]
    public required List<GetFileResponse> FileList { get; init; } = [];
};