namespace Logic.Records.Detect.Params;

/// <summary>
/// Параметры для задач обнаружения
/// </summary>
public class DetectParam
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