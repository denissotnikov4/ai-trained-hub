using Newtonsoft.Json;

namespace Api.Controllers.Pose.Dto.Responses;

/// <summary>
/// Ответ на задачу типа Pose
/// </summary>
public record PoseResponse
{
    /// <summary>
    /// Идентификатор файла с предиктом
    /// </summary>
    [JsonProperty("predictedFileId")]
    public Guid PredictedFileId { get; init; }
}