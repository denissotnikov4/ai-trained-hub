using Dal.Enums;

namespace Api.Controllers.File.Dto.Request;

/// <summary>
/// Запрос на загрузку файла
/// </summary>
public record CreateFileRequest
{
    /// <summary>
    /// Тип доступа до файла
    /// </summary>
    public required AccessModifier AccessModifier { get; init; }
};