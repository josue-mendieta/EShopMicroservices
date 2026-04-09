namespace Catalog.API.Products.GetProductById;

/// <summary>
/// Represents a query to retrieve a product by its unique identifier.
/// Representa una consulta para recuperar un producto por su identificador único.
/// </summary>
/// <param name="Id">
/// The unique identifier of the product to retrieve.
/// El identificador único del producto a recuperar.
/// </param>
public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

/// <summary>
/// Represents the result of a product retrieval operation by ID, containing the requested product information.
/// Representa el resultado de una operación de recuperación de productos por ID, que contiene la información del producto solicitado.
/// </summary>
/// <param name="Product">
/// The product instance returned by the retrieval operation. Cannot be null.
/// El producto devuelto por la operación de recuperación. No puede ser nulo.
/// </param>
public record GetProductByIdResult(Product Product);

/// <summary>
/// Handles queries to retrieve a product by its unique identifier.
/// Maneja consultas para recuperar un producto por su identificador único.
/// </summary>
/// <remarks>
/// This handler is intended for internal use within the application's query processing pipeline. 
/// Este manejador está destinado para uso interno dentro de la canalización de procesamiento de consultas de la aplicación.
/// It requires a valid document session and logger to operate correctly.
/// Este requiere una sesión de documentos válida y un registrador para operar correctamente.
/// </remarks>
/// <param name="session">
/// The document session used to access and load product data from the data store.
/// El sesión de documentos utilizada para acceder y cargar datos de productos desde el almacén de datos.
/// </param>
internal class GetProductByIdQueryHandler(    
    IDocumentSession session
    ) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    /// <summary>
    /// Handles a request to retrieve a product by its identifier.
    /// Maneja una solicitud para recuperar un producto por su identificador.
    /// </summary>
    /// <param name="query">
    /// The query containing the identifier of the product to retrieve. Cannot be null.
    /// La consulta que contiene el identificador del producto a recuperar. No puede ser nulo.
    /// </param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A result object containing the requested product. The result includes product details if found.</returns>
    /// <exception cref="ProductNotFoundException">Thrown if no product exists with the specified identifier.</exception>
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {        
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if (product == null)
        {
            throw new ProductNotFoundException(query.Id);
        }

        return new GetProductByIdResult(product);
    }
}
