namespace Logic.Records.Pose.Results;

/// <summary>
/// Результат задачи типа Pose
/// </summary>
public class PoseResult
{
    /// <summary>
    /// Идентификатор файла с предиктом
    /// </summary>
    public required Guid PredictedFileId { get; init; }
}