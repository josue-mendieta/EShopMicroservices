
namespace Catalog.API.Products.GetProductByCategory;

//record GetProductByCategoryRequest(string Category);

/// <summary>
/// Represents the response containing the collection of products that belong to a specified category.
/// Representa la respuesta que contiene la colección de productos que pertenecen a una categoría especificada.
/// </summary>
/// <param name="Products">
/// The collection of products returned for the requested category. Cannot be null.
/// La colección de productos devueltos para la categoría solicitada. No puede ser nulo.
/// </param>
record GetProductByCategoryResponse(IEnumerable<Product> Products);

/// <summary>
/// Defines an endpoint for retrieving products from the catalog that belong to a specified category.
/// Define un endpoint para recuperar productos del catálogo que pertenecen a una categoría especificada.
/// </summary>
/// <remarks>
/// This endpoint registers a GET route at "/products/category/{category}".
/// Este endpoint registra una ruta GET en "/products/category/{category}".
/// 
/// It is intended to be used within an ASP.NET Core application using Carter for minimal API routing. 
/// Está destinado a ser utilizado dentro de una aplicación ASP.NET Core que utiliza Carter para el enrutamiento de API mínima.
/// 
/// The endpoint returns a list of products matching the given category. 
/// El endpoint devuelve una lista de productos que coinciden con la categoría dada.
/// 
/// The response includes a 200 status code with the product list on success, a 400 status code for invalid requests, and a 500 status code for server errors.
/// La respuesta incluye un código de estado 200 con la lista de productos en caso de éxito, un código de estado 400 para solicitudes inválidas y un código de estado 500 para errores del servidor.
/// </remarks>
public class GetProductByCategoryEndpoint : ICarterModule
{
    /// <summary>
    /// Adds product-related endpoints to the specified endpoint route builder.
    /// Agrega endpoints relacionados con productos al constructor de rutas de endpoint especificado.
    /// </summary>
    /// <remarks>
    /// This method registers endpoints for retrieving products by category. 
    /// Este método registra endpoints para recuperar productos por categoría.
    /// 
    /// It should be called during application startup to ensure the routes are available.
    /// Debe ser llamado durante el inicio de la aplicación para asegurar que las rutas estén disponibles.
    /// </remarks>
    /// <param name="app">
    /// The endpoint route builder to which the product endpoints will be added. Cannot be null.
    /// El constructor de rutas de endpoint al que se agregarán los endpoints de productos. No puede ser nulo.
    /// </param>
    public void AddRoutes(IEndpointRouteBuilder app)
    {        
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
        {
            var query = new GetProductByCategoryQuery(category);
            var result = await sender.Send(query);
            var response = result.Adapt<GetProductByCategoryResponse>();
            return Results.Ok(response);
        })
            .WithName("GetProductByCategory")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Get products by category")
            .WithDescription("Gets a list of products from the catalog that belong to the specified category.");
    }
}