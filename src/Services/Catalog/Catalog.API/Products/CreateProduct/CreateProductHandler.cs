namespace Catalog.API.Products.CreateProduct;

/// <summary>
/// Registro que representa el comando para crear un nuevo producto en el sistema, heredando de IRequest de MediatR para definir el tipo de resultado esperado.
/// </summary>
/// <param name="Name"></param>
/// <param name="Category"></param>
/// <param name="Description"></param>
/// <param name="ImageFile"></param>
/// <param name="Price"></param>
public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;

/// <summary>
/// Registro que representa el resultado de la creación de un nuevo producto en el sistema.
/// </summary>
/// <param name="Id"></param>
public record CreateProductResult(Guid Id);

/// <summary>
/// Clase responsable de manejar la creación de productos, 
/// según el patrón CQRS es el handler del comando para crear un nuevo producto en el sistema 
/// (logica de negocio para crear el producto).
/// </summary>
internal class CreateProductHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    /// <summary>
    /// Método encargado de manejar la lógica para crear un nuevo producto en el sistema.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {

        //Create product entity from command object 
        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        //TODO: Save product entity to database
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        //Return CreateProductResult with new product id
        return new CreateProductResult(product.Id);

    }
}
