using Newtonsoft.Json;

namespace Api.Controllers.Dto.Response;

/// <summary>
/// Ответ на создание файла
/// </summary>
public record CreateFileResponse
{
    /// <summary>
    /// Идентификатор файла
    /// </summary>
    [JsonProperty("fileId")]
    public required Guid FileId { get; init; }
};