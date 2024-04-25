using System;

namespace HttpLogic.Models;

/// <summary>
/// Запрос для загрузки файла
/// </summary>
public class HttpUploadedFile
{
    /// <summary>
    /// Идентификатор файла
    /// </summary>
    public required Guid FileId { get; set; }
}