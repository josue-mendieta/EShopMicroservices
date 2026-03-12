using BuildingBlocks.Exceptions;

namespace Catalog.API.Exceptions;

[Serializable]
internal class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid id) : base("Product not found!", id)
    {
    }

    public ProductNotFoundException(string message) : base(message)
    {
    }

    public ProductNotFoundException(string message, Exception? innerException) : base(message, innerException)
    {
    }
}