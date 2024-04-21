using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.File.Dto.Response;

/// <summary>
/// Ответ на получение файла
/// </summary>
public record GetFileResponse
{
    /// <summary>
    /// Имя файла
    /// </summary>
    [JsonProperty("fileName")]
    public required string FileName { get; init; }

    /// <summary>
    /// Расширение файла
    /// </summary>
    [JsonProperty("fileExtension")]
    public required string FileExtension { get; init; }

    /// <summary>
    /// Время создания файла
    /// </summary>
    [JsonProperty("createdTime")]
    public required DateTime CreatedTime { get; init; }

    /// <summary>
    /// Размер файла
    /// </summary>
    [JsonProperty("fileSize")]
    public required int FileSize { get; init; }
};