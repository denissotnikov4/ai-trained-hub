namespace Logic.Records.File.Results;

/// <summary>
/// Результат получения списка файлов
/// </summary>
public record GetFileListResult
{
    /// <summary>
    /// Список файлов
    /// </summary>
    public List<GetFileResult> FileList { get; init; } = new();
};