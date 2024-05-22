using Newtonsoft.Json;

namespace Api.Controllers.Classify.Dto.Responses;

/// <summary>
/// Ответ на задачу классификации
/// </summary>
public record ClassifyResponse
{
    /// <summary>
    /// Идентификатор файла с предиктом
    /// </summary>
    [JsonProperty("predictedFileId")]
    public Guid PredictedFileId { get; init; }
}