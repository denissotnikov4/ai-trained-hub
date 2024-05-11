using Dal.Enums;
using Newtonsoft.Json;

namespace Api.Controllers.File.Dto.Request;

/// <summary>
/// Запрос на загрузку файла
/// </summary>
public record CreateFileRequest
{
    /// <summary>
    /// Тип доступа до файла
    /// </summary>
    [JsonProperty("accessModifier")]
    public required AccessModifier AccessModifier { get; init; }
};