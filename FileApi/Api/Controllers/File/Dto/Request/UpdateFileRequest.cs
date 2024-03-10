using Dal.Enums;

namespace Api.Controllers.File.Dto.Request;

/// <summary>
/// Запрос на обновление файла
/// </summary>
public record UpdateFileRequest
{
    /// <summary>
    /// Идентификатор файла
    /// </summary>
    public required Guid FileId { get; init; }

    /// <summary>
    /// Модификатор доступа к файлу
    /// </summary>
    /// <returns></returns>
    public required AccessModifier AccessModifier { get; init; }
};