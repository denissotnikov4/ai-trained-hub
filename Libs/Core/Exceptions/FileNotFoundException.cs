using Core.Exceptions.Base;

namespace Core.Exceptions;

/// <summary>
/// Ошибка для кейса, когда файл не был найден
/// </summary>
public class FileNotFoundException : BaseCustomException
{
    public FileNotFoundException() : base("Файл не был найден")
    {
    }
    
    public FileNotFoundException(string message) : base(message)
    {
    }
}