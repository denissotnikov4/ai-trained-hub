using Logic.Records.File.Params;
using Logic.Records.File.Results;
using Microsoft.AspNetCore.Http;

namespace Logic.Managers.File.Managers.Interfaces;

/// <summary>
/// Интерфейс, определяющий контракт для менеджера файлов, предоставляющего операции для работы с файлами.
/// </summary>
public interface IFileManager
{
    /// <summary>
    /// Загружает файл на сервер.
    /// </summary>
    /// <param name="file">Объект IFormFile, представляющий загружаемый файл.</param>
    /// <returns>Результат загрузки файла, содержащий информацию о созданном файле.</returns>
    Task<CreateFileResult> UploadFileAsync(IFormFile file);
    
    /// <summary>
    /// Получает файл по его уникальному идентификатору.
    /// </summary>
    /// <param name="fileId">Уникальный идентификатор файла.</param>
    /// <returns>Результат получения файла, содержащий информацию о файле и его содержимое.</returns>
    Task<GetFileResult> GetFileByIdAsync(Guid fileId);

    /// <summary>
    /// Обновляет файл на сервере.
    /// </summary>
    /// <param name="updateFileParam">Параметры обновления файла, включая его уникальный идентификатор.</param>
    /// <param name="file">Объект IFormFile, представляющий обновляемый файл.</param>
    /// <returns>Задача, завершающаяся после успешного обновления файла.</returns>
    Task UpdateFileAsync(UpdateFileParam updateFileParam, IFormFile file);
    
    /// <summary>
    /// Удаляет файл с сервера по его уникальному идентификатору.
    /// </summary>
    /// <param name="fileId">Уникальный идентификатор файла, который нужно удалить.</param>
    /// <returns>Задача, завершающаяся после успешного удаления файла.</returns>
    Task DeleteFileAsync(Guid fileId);
}