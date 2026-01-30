
namespace Catalog.API.Products.GetProducts;


/// <summary>
/// Represents the response containing a collection of products returned from a query or service operation.
/// Representa la respuesta que contiene una colección de productos devueltos por una consulta u operación de servicio.
/// </summary>
/// <param name="Products">
/// The collection of products included in the response. Cannot be null; may be empty if no products are found.
/// La colección de productos incluida en la respuesta. No puede ser nulo; puede estar vacío si no se encuentran productos.
/// </param>
public record ProductsResponse(IEnumerable<Product> Products);

/// <summary>
/// Configures the endpoint for retrieving all products from the catalog.
/// Configura el endpoint para recuperar todos los productos del catálogo.
/// </summary>
/// <remarks>This class defines the route for the HTTP GET request to '/products', which returns a list of products. 
/// Esta clase define la ruta para la solicitud HTTP GET a '/products', que devuelve una lista de productos.
/// 
/// The endpoint responds with a 200 OK status and a list of products if successful, or with appropriate error responses for invalid requests or server errors. 
/// El endpoint responde con un estado 200 OK y una lista de productos si tiene éxito, o con respuestas de error apropiadas para solicitudes inválidas o errores del servidor.
/// 
/// Use this module to expose product data in an API conforming to Carter's routing conventions.
/// Use este módulo para exponer datos de productos en una API que cumpla con las convenciones de enrutamiento de Carter.
/// </remarks>
public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        //GET /products
        app.MapGet("/products", async (ISender sender) =>
        {
            var query = new GetProductsQuery();
            var result = await sender.Send(query);
            var response = result.Adapt<ProductsResponse>();
            return Results.Ok(response);
        })
            .WithName("GetProducts")
            .Produces<ProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Get all products")
            .WithDescription("Gets a list of all products available in the catalog.")
            ;
    }
}
