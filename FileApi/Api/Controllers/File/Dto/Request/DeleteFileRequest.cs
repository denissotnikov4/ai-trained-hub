using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Api.Controllers.File.Dto.Request;

/// <summary>
/// Запрос на удаление файла
/// </summary>
public record DeleteFileRequest
{
    /// <summary>
    /// Идентификатор файла
    /// </summary>
    [JsonProperty("fileId")]
    public required Guid FileId { get; init; }
};