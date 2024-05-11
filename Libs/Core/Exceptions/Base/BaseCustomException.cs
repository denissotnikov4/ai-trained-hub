namespace Core.Exceptions.Base;

/// <summary>
/// Базовый класс кастомного исключения
/// </summary>
public class BaseCustomException : Exception
{
    public BaseCustomException(string message) : base(message)
    {
    }
}