namespace Catalog.API.Products.UpdateProduct;

/// <summary>
/// Represents a command to update the details of an existing product.
/// Representa un comando para actualizar los detalles de un producto existente.
/// </summary>
/// <param name="Id">The unique identifier of the product to update.</param>
/// <param name="Name">The new name to assign to the product. Cannot be null or empty.</param>
/// <param name="Category">A list of category names to associate with the product. Cannot be null; may be empty if the product should have no
/// categories.</param>
/// <param name="Description">The updated description of the product. Cannot be null.</param>
/// <param name="ImageFile">The file name or path of the product's image. Cannot be null.</param>
/// <param name="Price">The new price to set for the product. Must be greater than or equal to zero.</param>
public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;

/// <summary>
/// Represents the result of an attempt to update a product.
/// Representa el resultado de un intento de actualizar un producto.
/// </summary>
/// <param name="IsSuccess">
/// true if the product update operation completed successfully; otherwise, false.
/// true si la operación de actualización del producto se completó con éxito; de lo contrario, false.
/// </param>
public record UpdateProductResult(bool IsSuccess);

/// <summary>
/// Handles the update operation for a product by processing an UpdateProductCommand and persisting changes to the data store.
/// Maneja la operación de actualización para un producto procesando un UpdateProductCommand y persistiendo los cambios en el almacén de datos.
/// </summary>
/// <param name="logger">The logger used to record informational and warning messages during command handling.</param>
/// <param name="session">The document session used to load, update, and save product data.</param>
internal class UpdateProductCommandHandler(
    ILogger<UpdateProductCommandHandler> logger,
    IDocumentSession session
    )
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductHandler.Handle called with {@Command}", command);

            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product == null)
        {
            logger.LogWarning("Product with id {ProductId} not found for update.", command.Id);
            throw new ProductNotFoundException();
        }

        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}
