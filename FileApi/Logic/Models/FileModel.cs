using Dal.Enums;

namespace Logic.Models;

public class FileModel
{
    /// <summary>
    /// Идентификатор файла
    /// </summary>
    public Guid FileId { get; init; }
    
    /// <summary>
    /// Изначальное имя файла
    /// </summary>
    public string OriginalFileName { get; init; }
    
    /// <summary>
    /// Обработанное имя файла
    /// </summary>
    public string HandledFileName { get; init; }

    /// <summary>
    /// Расширение файла
    /// </summary>
    public string FileExtension { get; init; }

    /// <summary>
    /// Время создания файла
    /// </summary>
    public DateTime? CreatedTime { get; init; }

    /// <summary>
    /// Размер файла
    /// </summary>
    public long FileSize { get; init; }

    /// <summary>
    /// Модификатор доступа к файлу
    /// </summary>
    public AccessModifier AccessModifier { get; init; }
}