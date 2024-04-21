using Core.Exceptions.Base;

namespace Core.Exceptions;

public class FileNotFoundException : BaseCustomException
{
    public FileNotFoundException(string message) : base(message)
    {
    }

    public FileNotFoundException() : base("File not found")
    {
    }
}