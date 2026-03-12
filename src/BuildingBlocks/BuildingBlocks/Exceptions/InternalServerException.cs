namespace BuildingBlocks.Exceptions;

[Serializable]
public class InternalServerException : Exception
{
    public InternalServerException() : base()
    {
    }

    public InternalServerException(string message) : base(message)
    {
    }

    public InternalServerException(string message, string details) : base(message)
    {
        Details = details;
    }


    public InternalServerException(string message, Exception? innerException) : base(message, innerException)
    {
    }

    public string? Details { get; set; }
}