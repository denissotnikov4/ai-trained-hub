using Logic.Records.Segment.Params;
using Logic.Records.Segment.Results;

namespace Logic.Managers.Segment.Interfaces;

/// <summary>
/// Менеджер для задачи сегментирования
/// </summary>
public interface ISegmentManager
{
    /// <summary>
    /// Сегментирование
    /// </summary>
    public Task<SegmentResult> SegmentAsync(SegmentParam segmentParam);
}