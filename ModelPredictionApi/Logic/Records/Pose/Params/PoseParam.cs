namespace Logic.Records.Pose.Params;

/// <summary>
/// Параметры для задач типа Pose
/// </summary>
public class PoseParam
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