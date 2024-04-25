using Dal.Enums;

namespace Logic.Records.File.Params;

/// <summary>
/// Параметры на обновление файла
/// </summary>
public record UpdateFileParam
{
    /// <summary>
    /// Идентификатор файла
    /// </summary>
    public Guid FileId { get; init; }
    
    /// <summary>
    /// Модификатор доступа к файлу
    /// </summary>
    /// <returns></returns>
    public AccessModifier AccessModifier { get; init; }
}