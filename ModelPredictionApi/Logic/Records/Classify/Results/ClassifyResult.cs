namespace Logic.Records.Classify.Results;

/// <summary>
/// Результат классификации
/// </summary>
public class ClassifyResult
{
    /// <summary>
    /// Идентификатор файла с предиктом
    /// </summary>
    public Guid PredictedFileId { get; init; }
}