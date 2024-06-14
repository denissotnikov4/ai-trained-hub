namespace Logic.Records.Classify.Params;

/// <summary>
/// Параметры для задачи классификации
/// </summary>
public class ClassifyParam
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