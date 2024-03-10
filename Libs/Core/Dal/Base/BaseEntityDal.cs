namespace Core.Dal.Base;

/// <summary>
/// Базовая сущность для работы с сущностями в бд
/// </summary>
public record BaseEntityDal<T>
{
    /// <summary>
    /// уникальный идентфиикатор сущности
    /// </summary>
    public T Id { get; init; }
}