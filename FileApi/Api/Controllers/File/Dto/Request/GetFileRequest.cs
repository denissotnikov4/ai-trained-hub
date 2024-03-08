namespace Api.Controllers.File.Dto.Request;

/// <summary>
/// Запрос на получение файла
/// </summary>
public record GetFileRequest
{
    /// <summary>
    /// Идентификатор файла
    /// </summary>
    public required Guid FileId { get; init; }
};