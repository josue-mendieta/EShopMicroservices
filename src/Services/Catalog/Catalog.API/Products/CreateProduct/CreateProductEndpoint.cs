namespace Catalog.API.Products.CreateProduct;


/// <summary>
/// Registro que representa la solicitud para crear un nuevo producto en el sistema.
/// </summary>
/// <param name="Name">Nombre del producto.</param>
/// <param name="Category">Categorías a las que pertenece el producto.</param>
/// <param name="Description">Descripción del producto.</param>
/// <param name="ImageFile">Imagen del producto.</param>
/// <param name="Price">Precio del producto.</param>
public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);

/// <summary>
/// Registro que representa la respuesta después de crear un nuevo producto en el sistema.
/// </summary>
/// <param name="Id">Identificador único del producto creado.</param>
public record CreateProductResponse(Guid Id);

/// <summary>
/// Defines the API endpoint for creating a new product.
/// Define el endpoint API para crear un nuevo producto.
/// </summary>
/// <remarks>This class registers the HTTP POST route for product creation and configures the expected request and response types. 
/// Esta clase registra la ruta HTTP POST para la creación de productos y configura los tipos de solicitud y respuesta esperados.
/// It is intended to be used with the Carter framework for minimal API endpoint definition.
/// Esta clase está destinada a ser utilizada con el framework Carter para la definición de endpoints de API mínima.
/// </remarks>
public class CreateProductEndpoint : ICarterModule
{
    /// <summary>
    /// Adds the product-related endpoints to the specified endpoint route builder.
    /// Agrega los endpoints relacionados con productos al constructor de rutas de endpoints especificado.
    /// </summary>
    /// <remarks>
    /// This method registers the route for creating a new product. 
    /// The endpoint expects a POST request to "/products" with the product details in the request body. 
    /// A successful request returns a 201 Created response with the created product; invalid requests return a 400 Bad Request with problem details.
    /// Este método registra la ruta para crear un nuevo producto.
    /// El endpoint espera una solicitud POST a "/products" con los detalles del producto en el cuerpo de la solicitud.
    /// Una solicitud exitosa devuelve una respuesta 201 Created con el producto creado; las solicitudes inválidas devuelven un 400 Bad Request con detalles del problema.
    /// </remarks>
    /// <param name="app">
    /// The endpoint route builder to which the product endpoints will be added. Cannot be null.
    /// El constructor de rutas de endpoints al que se agregarán los endpoints de productos. No puede ser nulo.
    /// </param>
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProductCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CreateProductResponse>();

            return Results.Created($"/products/{response.Id}", response);
        })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Creates a new product in the system")
            .WithDescription("Creates a new product in the system with the provided details.")
            ;        
    }
}
