namespace Logic.Records.File.Results;

/// <summary>
/// Результат создания файла
/// </summary>
public record CreateFileResult
{
    /// <summary>
    /// Идентификатор файла
    /// </summary>
    public Guid FileId { get; init; }
};