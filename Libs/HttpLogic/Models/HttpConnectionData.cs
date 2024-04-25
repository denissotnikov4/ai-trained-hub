using System;

namespace HttpLogic.Models;

/// <summary>
/// Структура, содержащая данные о соединении HTTP, включая имя клиента и таймаут.
/// </summary>
public readonly struct HttpConnectionData
{
    /// <summary>
    /// Имя клиента, используемое для идентификации соединения.
    /// </summary>
    public string? ClientName { get; init; }

    /// <summary>
    /// Таймаут для соединения, определяющий максимальное время ожидания ответа от сервера.
    /// </summary>
    public TimeSpan? TimeOut { get; init; }
}
