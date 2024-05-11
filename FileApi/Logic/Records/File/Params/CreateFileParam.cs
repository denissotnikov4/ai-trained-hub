using Dal.Enums;

namespace Logic.Records.File.Params;

/// <summary>
/// Параметры для создания файла
/// </summary>
public class CreateFileParam
{
    /// <summary>
    /// Модификатор доступа к файлу
    /// </summary>
    public AccessModifier AccessModifier { get; init; }
}