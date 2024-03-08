namespace Logic.Records.File.Results;

/// <summary>
/// Результат получения файла по id
/// </summary>
public record GetFileResult
{
    /// <summary>
    /// Изначальное имя файла
    /// </summary>
    public string OriginalFileName { get; set; }


    /// <summary>
    /// Обработанное имя файла
    /// </summary>
    public string HandledFileName { get; set; }

    /// <summary>
    /// Расширение файла
    /// </summary>
    public string FileExtension { get; init; }

    /// <summary>
    /// Время создания файла
    /// </summary>
    public DateTime CreatedTime { get; init; }

    /// <summary>
    /// Размер файла
    /// </summary>
    public long FileSize { get; init; }

    /// <summary>
    /// Stream файла
    /// </summary>
    public Stream FileStream { get; set; }
}