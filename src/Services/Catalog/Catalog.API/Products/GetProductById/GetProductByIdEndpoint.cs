namespace Catalog.API.Products.GetProductById;

/// <summary>
/// Represents the response returned when retrieving a product by its identifier.
/// Representa la respuesta devuelta al recuperar un producto por su identificador.
/// </summary>
/// <param name="Product">
/// The product associated with the specified identifier. Cannot be null.
/// El producto asociado con el identificador especificado. No puede ser nulo.
/// </param>
public record GetProductByIdResponse(Product Product);

/// <summary>
/// Defines an endpoint for retrieving a single product from the catalog by its unique identifier.
/// Define un endpoint para recuperar un solo producto del catálogo por su identificador único.
/// </summary>
/// <remarks>
/// This endpoint handles HTTP GET requests to "/products/{id}" where "id" is a GUID representing the product's unique identifier. 
/// Este endpoint maneja solicitudes HTTP GET a "/products/{id}" donde "id" es un GUID que representa el identificador único del producto.
/// 
/// It returns a 200 OK response with the product details if found, a 400 BadRequest for invalid input, a 404 NotFound if the product does not exist, and a 500 InternalServerError for unexpected failures.
/// Retorna una respuesta 200 OK con los detalles del producto si se encuentra, un 400 BadRequest para entradas inválidas, un 404 NotFound si el producto no existe, y un 500 InternalServerError para fallos inesperados.
/// 
/// Use this endpoint to obtain detailed information about a specific product in the catalog.
/// Use este endpoint para obtener información detallada sobre un producto específico en el catálogo.
/// </remarks>
public class GetProductByIdEndpoint : ICarterModule
{
    /// <summary>
    /// Configures product-related HTTP endpoints for the application, including a route to retrieve a product by its unique identifier.
    /// Configura los endpoints HTTP relacionados con productos para la aplicación, incluyendo una ruta para recuperar un producto por su identificador único.
    /// </summary>
    /// <remarks>This method adds a GET endpoint at '/products/{id:guid}' that returns product details for the
    /// specified product ID. The endpoint supports standard HTTP status codes for success and error conditions,
    /// including 200 (OK), 400 (Bad Request), 404 (Not Found), and 500 (Internal Server Error).</remarks>
    /// <param name="app">The endpoint route builder used to define and register HTTP routes for the application.</param>
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // GET /products/{id}
        app.MapGet("/products/{id:guid}", async (Guid id, ISender sender) =>
        {
            var query = new GetProductByIdQuery(id);
            var result = await sender.Send(query);
            var response = result.Adapt<GetProductByIdResponse>();
            return Results.Ok(response);
        })
            .WithName("GetProductById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Get product by ID")
            .WithDescription("Gets a single product from the catalog by its unique identifier.");
    }
}
