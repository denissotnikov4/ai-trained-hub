namespace Api.Controllers.Dto.Response;

/// <summary>
/// Ответ на создание файла
/// </summary>
public record CreateFileResponse
{
    /// <summary>
    /// Идентификатор файла
    /// </summary>
    public required Guid FileId { get; set; }
};