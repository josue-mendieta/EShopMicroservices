
namespace Catalog.API.Products.DeleteProduct;

/// <summary>
/// Represents a command to delete a product identified by its unique identifier.
/// Representa un comando para eliminar un producto identificado por su identificador único.
/// </summary>
/// <param name="Id">
/// The unique identifier of the product to delete.
/// El identificador único del producto a eliminar.
/// </param>
record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

/// <summary>
/// Represents the result of a product deletion operation, indicating whether the deletion was successful.
/// Representa el resultado de una operación de eliminación de producto, indicando si la eliminación fue exitosa.
/// </summary>
/// <param name="IsSuccess">
/// true if the product was deleted successfully; otherwise, false.
/// true si el producto fue eliminado con éxito; de lo contrario, false.
/// </param>
record DeleteProductResult(bool IsSuccess);

/// <summary>
/// Handles commands to delete a product from the data store.
/// Maneja comandos para eliminar un producto del almacén de datos.
/// </summary>
/// <param name="logger">The logger used to record informational and warning messages during command handling.</param>
/// <param name="session">The document session used to access and modify product data in the data store.</param>
internal class DeleteProductCommandHandler(
    ILogger<DeleteProductCommandHandler> logger,
    IDocumentSession session
    ) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    /// <summary>
    /// Handles the deletion of a product based on the specified command.
    /// Maneja la eliminación de un producto basado en el comando especificado.
    /// </summary>
    /// <param name="command">
    /// The command containing the identifier of the product to delete.
    /// El comando que contiene el identificador del producto a eliminar.
    /// </param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the delete operation.</param>
    /// <returns>A result indicating whether the product was successfully deleted.</returns>
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteProductCommandHandler.Handle called with {@Command}", command);

        session.Delete<Product>(command.Id);

        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}
