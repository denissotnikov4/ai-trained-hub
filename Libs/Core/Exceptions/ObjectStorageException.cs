using Core.Exceptions.Base;

namespace Core.Exceptions;

public class ObjectStorageException : BaseCustomException
{
    public ObjectStorageException(string message) : base(message)
    {
    }

    public ObjectStorageException() : base("Произошла ошибка с хранилищем файлов")
    {
    }
}