using Newtonsoft.Json;

namespace Api.Controllers.Classify.Dto.Requests;

/// <summary>
/// Запрос для задач классификации
/// </summary>
public record ClassifyRequest
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