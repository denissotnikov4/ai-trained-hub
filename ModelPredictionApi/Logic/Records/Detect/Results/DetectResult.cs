namespace Logic.Records.Detect.Results;

/// <summary>
/// Результат обнаружения
/// </summary>
public class DetectResult
{
    /// <summary>
    /// Идентфикатор файла с предсказанием
    /// </summary>
    public required Guid PredictedFileId { get; set; }
}