
namespace Catalog.API.Products.UpdateProduct;

/// <summary>
/// Represents a request to update the details of an existing product.
/// Representa una solicitud para actualizar los detalles de un producto existente.
/// </summary>
/// <param name="Id">The unique identifier of the product to update.</param>
/// <param name="Name">The new name to assign to the product. Cannot be null or empty.</param>
/// <param name="Category">A list of category names to associate with the product. Cannot be null; may be empty if the product should have no
/// categories.</param>
/// <param name="Description">The updated description of the product. Cannot be null; may be empty if no description is desired.</param>
/// <param name="ImageFile">The file name or path of the product's image. Cannot be null or empty.</param>
/// <param name="Price">The new price to set for the product. Must be a non-negative value.</param>
public record UpdateProductRequest(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);

/// <summary>
/// Represents the result of an attempt to update a product.
/// Representa el resultado de un intento de actualizar un producto.
/// </summary>
/// <param name="IsSuccess">A value indicating whether the product update operation was successful. Set to <see langword="true"/> if the update
/// succeeded; otherwise, <see langword="false"/>.</param>
public record UpdateProductResponse(bool IsSuccess);

/// <summary>
/// Defines an endpoint for updating the details of an existing product in the catalog.
/// Define un endpoint para actualizar los detalles de un producto existente en el catálogo.
/// </summary>
/// <remarks>
/// This endpoint registers a HTTP PUT route for updating product information. 
/// Este endpoint registra una ruta HTTP PUT para actualizar la información del producto.
/// 
/// It expects a JSON payload containing the updated product details and returns the updated product data if the operation is successful. 
/// Espera una carga JSON que contenga los detalles actualizados del producto y devuelve los datos del producto actualizado si la operación es exitosa.
/// 
/// If the specified product does not exist, a 404 Not Found response is returned. 
/// Si el producto especificado no existe, se devuelve una respuesta 404 Not Found.
/// 
/// If the request is invalid, a 400 Bad Request response is returned.
/// Si la solicitud es inválida, se devuelve una respuesta 400 Bad Request.
/// </remarks>
public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("products", 
            async (UpdateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProductCommand>();
                var result = await sender.Send(command);

                var response = result.Adapt<UpdateProductResponse>();

                return Results.Ok(response);

            })
            .Accepts<UpdateProductRequest>("application/json")
            .WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)            
            .WithSummary("Update product")
            .WithDescription("Updates the details of an existing product in the catalog by its unique identifier.");
    }
}
