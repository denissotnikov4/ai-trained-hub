using Core.Exceptions.Base;

namespace Core.Exceptions;

public class ObjectStorageException : BaseCustomException
{
    public ObjectStorageException() : base("Произошла ошибка с хранилищем файлов")
        {
        }
    
    public ObjectStorageException(string message) : base(message)
    {
    }
}