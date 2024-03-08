using Dal.Enums;

namespace Logic.Models;

public class FileModel
{
    /// <summary>
    /// Идентификатор файла
    /// </summary>
    public Guid FileId { get; set; }
    
    /// <summary>
    /// Изначальное имя файла
    /// </summary>
    public string OriginalFileName { get; set; }


    /// <summary>
    /// Обработанное имя файла
    /// </summary>
    public string HandledFileName { get; set; }

    /// <summary>
    /// Расширение файла
    /// </summary>
    public string FileExtension { get; set; }

    /// <summary>
    /// Время создания файла
    /// </summary>
    public DateTime? CreatedTime { get; set; }

    /// <summary>
    /// Размер файла
    /// </summary>
    public long FileSize { get; set; }

    /// <summary>
    /// Модификатор доступа к файлу
    /// </summary>
    public AccessModifier AccessModifier { get; set; }
}