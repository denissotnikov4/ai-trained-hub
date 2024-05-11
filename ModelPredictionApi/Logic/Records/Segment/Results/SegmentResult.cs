namespace Logic.Records.Segment.Results;

/// <summary>
/// Результат работы задачи сегментирования
/// </summary>
public class SegmentResult
{
    /// <summary>
    /// Идентификатор файла с предиктом
    /// </summary>
    public required Guid PredictedFileId { get; init; }
}