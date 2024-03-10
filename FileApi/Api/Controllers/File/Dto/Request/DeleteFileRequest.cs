namespace Api.Controllers.File.Dto.Request;

/// <summary>
/// Запрос на удаление файла
/// </summary>
public record DeleteFileRequest
{
    /// <summary>
    /// Идентификатор файла
    /// </summary>
    public required Guid FileId { get; init; }
};