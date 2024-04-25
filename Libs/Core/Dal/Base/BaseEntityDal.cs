namespace Core.Dal.Base;

/// <summary>
/// Базовая сущность для работы с сущностями в бд
/// </summary>
public record BaseEntityDal<T>
{
    /// <summary>
    /// Уникальный идентфикатор сущности
    /// </summary>
    public T Id { get; init; }
}