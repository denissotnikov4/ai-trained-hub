namespace FileLib;

/// <summary>
/// Ответ на получение файла
/// </summary>
public record FileResponse
{
    /// <summary>
    /// Полное название файла
    /// </summary>
    public required string FullFileName { get; init; }

    /// <summary>
    /// Массив байтов файла
    /// </summary>
    public required byte[] FileContent { get; init; }
};