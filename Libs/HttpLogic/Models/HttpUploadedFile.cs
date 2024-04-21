using System;

namespace HttpLogic.Models;

/// <summary>
/// Запрос для загрузки файла
/// </summary>
public class HttpUploadedFile
{
    /// <summary>
    /// File content
    /// </summary>
    public required Guid FileId { get; set; }
}