namespace HttpLogic.Models;

/// <summary>
/// Файл, полученный в результате HTTP-запроса
/// </summary>
public record HttpDownloadedFile
{
    /// <summary>
    /// Full file name
    /// </summary>
    public string FileNameWithExtension { get; init; }

    /// <summary>
    /// File content
    /// </summary>
    public byte[] FileContent { get; init; }
}