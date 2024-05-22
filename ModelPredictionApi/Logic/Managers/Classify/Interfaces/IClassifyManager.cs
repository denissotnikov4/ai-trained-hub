using Logic.Records.Classify.Params;
using Logic.Records.Classify.Results;

namespace Logic.Managers.Classify.Interfaces;

/// <summary>
/// Менеджер для задач классификации
/// </summary>
public interface IClassifyManager
{
    /// <summary>
    /// Сделать предикт для задач классификация
    /// </summary>
    Task<ClassifyResult> ClassifyAsync(ClassifyParam classifyParam);
}