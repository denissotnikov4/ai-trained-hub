using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace Api.Controllers.Segment.Dto.Responses;

/// <summary>
/// Ответ на задачу сегментирования
/// </summary>
public record SegmentResponse
{
    /// <summary>
    /// Идентификатор файла с предиктом
    /// </summary>
    [JsonProperty("predictedFileId")]
    public Guid PredictedFileId { get; init; }
}