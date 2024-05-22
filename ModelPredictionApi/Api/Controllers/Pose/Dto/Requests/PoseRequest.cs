using Newtonsoft.Json;

namespace Api.Controllers.Pose.Dto.Requests;

/// <summary>
/// Запрос для задач типа Pose
/// </summary>
public record PoseRequest
{
    /// <summary>
    /// Идентификатор обученной модели
    /// </summary>
    [JsonProperty("modelId")]
    public required Guid ModelId { get; init; }

    /// <summary>
    /// Идентификатор файла, на котором будет производиться предикт
    /// </summary>
    [JsonProperty("fileId")]
    public required Guid FileId { get; init; }
}