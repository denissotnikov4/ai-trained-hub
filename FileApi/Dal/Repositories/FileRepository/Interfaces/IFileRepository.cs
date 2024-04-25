using Dal.Models;

namespace Dal.Repositories.FileRepository.Interfaces;

/// <summary>
/// Репозиторий для <see cref="FileDal"/>
/// </summary>
public interface IFileRepository
{
    /// <summary>
    /// Сохраняет файл в хранилище и возвращает его уникальный идентификатор.
    /// </summary>
    /// <param name="fileDal">Объект, содержащий данные файла для сохранения.</param>
    /// <returns>Уникальный идентификатор сохраненного файла.</returns>
    Task<Guid> SaveFileAsync(FileDal fileDal);

    /// <summary>
    /// Получает файл по его уникальному идентификатору.
    /// </summary>
    /// <param name="fileId">Уникальный идентификатор файла.</param>
    /// <returns>Объект, содержащий данные файла.</returns>
    Task<FileDal> GetFileByIdAsync(Guid fileId);

    /// <summary>
    /// Обновляет данные файла в хранилище.
    /// </summary>
    /// <param name="fileDal">Объект с обновленными данными файла.</param>
    Task UpdateFileAsync(FileDal fileDal);
    
    /// <summary>
    /// Удаляет файл из хранилища по его уникальному идентификатору.
    /// </summary>
    /// <param name="fileId">Уникальный идентификатор файла для удаления.</param>
    Task DeleteFileAsync(Guid fileId);
}