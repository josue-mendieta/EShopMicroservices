namespace Catalog.API.Exceptions;

[Serializable]
internal class ProductNotFoundException : Exception
{
    public ProductNotFoundException() : base("Product not found!")
    {
    }

    public ProductNotFoundException(string? message) : base(message)
    {
    }

    public ProductNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}