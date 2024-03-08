using Dal.Models;

namespace Dal.Repositories.FileRepository.Interfaces;

/// <summary>
/// Репозиторий для <see cref="FileDal"/>
/// </summary>
public interface IFileRepository
{
    /// <summary>
    /// Создать файл
    /// </summary>
    Task<Guid> SaveFileAsync(FileDal fileDal);

    /// <summary>
    /// Получить файл
    /// </summary>
    Task<FileDal> GetFileByIdAsync(Guid fileId);

    /// <summary>
    /// Обновить файл
    /// </summary>
    Task UpdateFileAsync(FileDal fileDal);
    
    /// <summary>
    /// Удалить файл 
    /// </summary>
    Task DeleteFileAsync(Guid fileId);
}