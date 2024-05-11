using Logic.Records.Pose.Params;
using Logic.Records.Pose.Results;

namespace Logic.Managers.Pose.Interfaces;

/// <summary>
/// Менеджер для задач типа Pose
/// </summary>
public interface IPoseManager
{
    /// <summary>
    /// Сделать предикт для задачи типа Pose
    /// </summary>
    Task<PoseResult> PoseAsync(PoseParam poseParam);
}