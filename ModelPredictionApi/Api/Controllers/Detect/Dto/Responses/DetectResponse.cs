using Newtonsoft.Json;

namespace Api.Controllers.Detect.Dto.Responses;

/// <summary>
/// Ответ на задачу обнаружения
/// </summary>
public record DetectResponse
{
    /// <summary>
    /// Идентфикатор файла с предсказанием
    /// </summary>
    [JsonProperty("predictedFileId")]
    public Guid PredictedFileId { get; init; }
}