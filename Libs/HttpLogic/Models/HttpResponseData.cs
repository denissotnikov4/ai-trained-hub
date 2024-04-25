namespace HttpLogic.Models;

/// <summary>
/// Запись, представляющая данные HTTP-ответа, включая статус, заголовки и содержимое, специализированная для работы с типизированным телом ответа.
/// </summary>
/// <typeparam name="TResponse">Тип данных, содержащихся в теле ответа.</typeparam>
public record HttpResponseData<TResponse> : BaseHttpResponse
{
    /// <summary>
    /// Тело ответа, содержащее данные, специфичные для типа TResponse.
    /// </summary>
    public TResponse? Body { get; init; }
}
