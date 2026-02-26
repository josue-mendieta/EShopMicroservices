
namespace Catalog.API.Products.DeleteProduct;

/// <summary>
/// Represents a request to delete a product identified by its unique identifier.
/// Representa una solicitud para eliminar un producto identificado por su identificador único.
/// </summary>
/// <param name="Id">
/// The unique identifier of the product to be deleted. Must not be empty.
/// El identificador único del producto a eliminar. No debe estar vacío.
/// </param>
record DeleteProductRequest(Guid Id);

/// <summary>
/// Represents the result of a product deletion operation.
/// Representa el resultado de una operación de eliminación de producto.
/// </summary>
/// <param name="IsSuccess">Indicates whether the product was successfully deleted. <see langword="true"/> if the deletion succeeded; otherwise,
/// <see langword="false"/>.</param>
record DeleteProductResponse(bool IsSuccess);

/// <summary>
/// Defines the endpoint for deleting a product by its unique identifier.
/// Define el endpoint para eliminar un producto por su identificador único.
/// </summary>
/// <remarks>
/// This class registers a DELETE HTTP route for removing products. 
/// Este clase registra una ruta HTTP DELETE para eliminar productos.
/// 
/// The endpoint expects a product ID in the route and returns a response indicating whether the deletion was successful. 
/// El endpoint espera un ID de producto en la ruta y devuelve una respuesta que indica si la eliminación fue exitosa.
/// 
/// Use this module to expose product deletion functionality in a Carter-based API.
/// Use este módulo para exponer la funcionalidad de eliminación de productos en una API basada en Carter.
/// </remarks>
public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id:guid}", async (Guid id, ISender sender) =>
        {
            var command = new DeleteProductCommand(id);
            var result = await sender.Send(command);
            var response = new DeleteProductResponse(result.IsSuccess);
            return Results.Ok(response);
        })            
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithName("DeleteProduct")
            .WithDescription("Deletes a product by its unique identifier.")
            ;
    }
}