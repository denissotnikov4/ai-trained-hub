namespace Api.Controllers.File.Dto.Response;

/// <summary>
/// Ответ на получение файла
/// </summary>
public record GetFileResponse
{
    /// <summary>
    /// Имя файла
    /// </summary>
    public required string FileName { get; init; }

    /// <summary>
    /// Расширение файла
    /// </summary>
    public required string FileExtension { get; init; }

    /// <summary>
    /// Время создания файла
    /// </summary>
    public required DateTime CreatedTime { get; init; }

    /// <summary>
    /// Размер файла
    /// </summary>
    public required int FileSize { get; init; }
};