
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
/// Provides validation rules for the CreateProductCommand to ensure that product creation requests contain all required
/// fields and valid values.
/// Proporciona reglas de validación para el CreateProductCommand para garantizar que las solicitudes de creación de productos contengan todos los campos requeridos y valores válidos.
/// </summary>
/// <remarks>
/// This validator enforces that the product name, category, description, image file, and price are specified and meet basic requirements. 
/// Este validador asegura que el nombre del producto, la categoría, la descripción, el archivo de imagen y el precio estén especificados y cumplan con los requisitos básicos.
/// Use this class to validate CreateProductCommand instances before processing them to prevent invalid product data from being submitted.
/// Use esta clase para validar las instancias de CreateProductCommand antes de procesarlas para evitar que se envíen datos de productos no válidos.
/// </remarks>
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateProductCommandValidator class, configuring validation rules for product
    /// creation commands.
    /// Inicializa una nueva instancia de la clase CreateProductCommandValidator, configurando reglas de validación para los comandos de creación de productos.
    /// </summary>
    /// <remarks>The validator enforces that the product name, category, description, image file, and price are provided and valid.
    /// El validador asegura que el nombre del producto, la categoría, la descripción, el archivo de imagen y el precio se proporcionen y sean válidos.
    /// Use this validator to ensure that product creation requests meet required criteria before processing.
    /// Use este validador para garantizar que las solicitudes de creación de productos cumplan con los criterios requeridos antes de procesarlas.
    /// </remarks>
    public CreateProductCommandValidator()
    {
        RuleFor(command => command.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(command => command.Category).NotEmpty().WithMessage("At least one category is required.");
        RuleFor(command => command.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(command => command.ImageFile).NotEmpty().WithMessage("Image file is required.");
        RuleFor(command => command.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");

    }
}


/// <summary>
/// Clase responsable de manejar la creación de productos, 
/// según el patrón CQRS es el handler del comando para crear un nuevo producto en el sistema 
/// (logica de negocio para crear el producto).
/// </summary>
internal class CreateProductCommandHandler(    
    IDocumentSession session    
    ) : ICommandHandler<CreateProductCommand, CreateProductResult>
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

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        //Return CreateProductResult with new product id
        return new CreateProductResult(product.Id);

    }
}
