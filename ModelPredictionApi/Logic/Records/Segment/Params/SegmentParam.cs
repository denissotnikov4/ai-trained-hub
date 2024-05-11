namespace Logic.Records.Segment.Params;

/// <summary>
/// Параметры для задачи сегментирования
/// </summary>
public class SegmentParam
{
    /// <summary>
    /// Идентификатор обученной модели
    /// </summary>
    public Guid ModelId { get; init; }

    /// <summary>
    /// Идентификатор файла, на котором будет производиться предикт
    /// </summary>
    public Guid FileId { get; init; }
}