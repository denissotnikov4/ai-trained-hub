using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.Detect.Dto.Requests;

/// <summary>
/// Запрос для задач обнаружения 
/// </summary>
public record struct DetectRequest
{
    /// <summary>
    /// Идентификатор обученной модели
    /// </summary>
    [JsonProperty("modelId")]
    [Required]
    public required Guid ModelId { get; init; }

    /// <summary>
    /// Идентификатор файла, на котором будет производиться предикт
    /// </summary>
    [JsonProperty("fileId")]
    [Required]
    public required Guid FileId { get; init; }
}