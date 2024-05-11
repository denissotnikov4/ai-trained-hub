using Dal.Enums;
using Newtonsoft.Json;

namespace Api.Controllers.File.Dto.Request;

/// <summary>
/// Запрос на обновление файла
/// </summary>
public record UpdateFileRequest
{
    /// <summary>
    /// Идентификатор файла
    /// </summary>
    [JsonProperty("fileId")]
    public required Guid FileId { get; init; }

    /// <summary>
    /// Модификатор доступа к файлу
    /// </summary>
    /// <returns></returns>
    [JsonProperty("accessModifier")]
    public required AccessModifier AccessModifier { get; init; }
};