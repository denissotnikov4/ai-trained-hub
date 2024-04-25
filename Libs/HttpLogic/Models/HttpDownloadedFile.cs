namespace HttpLogic.Models;

/// <summary>
/// Файл, полученный в результате HTTP-запроса
/// </summary>
public record HttpDownloadedFile
{
    /// <summary>
    /// Полное название файла, включающее расширение 
    /// </summary>
    public string FileNameWithExtension { get; init; }

    /// <summary>
    /// Массив байтов файла
    /// </summary>
    public byte[] FileContent { get; init; }
}