using Logic.Records.Detect.Params;
using Logic.Records.Detect.Results;

namespace Logic.Managers.Detect.Interfaces;

/// <summary>
/// Менеджер для задач обнаружения
/// </summary>
public interface IDetectManager
{
    /// <summary>
    /// Сделать предикт для задач обнаружения
    /// </summary>
    public Task<DetectResult> DetectAsync(DetectParam detectParam);
}