using Core.Exceptions.Base;

namespace Core.Exceptions;

/// <summary>
/// Ошибка для кейса, когда переданная модель не поддерживает тип задачи
/// </summary>
public class ModelDoesNotSupportTaskException : BaseCustomException
{
    public ModelDoesNotSupportTaskException() : base("Данная модель не поддерживает этот тип задачи")
    {
    }

    public ModelDoesNotSupportTaskException(string message) : base(message)
    {
    }
}