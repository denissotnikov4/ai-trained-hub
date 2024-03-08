namespace Logic.Records.File.Results;

/// <summary>
/// Результат создания файла
/// </summary>
public record CreateFileResult
{
    /// <summary>
    /// Идентификатор файла
    /// </summary>
    public required Guid FileId { get; init; }
};