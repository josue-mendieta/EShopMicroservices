namespace Catalog.API.Products.GetProducts;


/// <summary>
/// Represents a query for retrieving a collection of products.
/// Representa una consulta para recuperar una colección de productos.
/// </summary>
public record GetProductsQuery() : IQuery<GetProductsResult>;

/// <summary>
/// Represents the result of a product retrieval operation, containing a collection of products.
/// Representa el resultado de una operación de recuperación de productos, que contiene una colección de productos.
/// </summary>
/// <param name="Products">
/// The collection of <see cref="Product"/> instances returned by the operation. Cannot be null; may be empty if no products are found.
/// La colección de instancias de <see cref="Product"/> devueltas por la operación. No puede ser nulo; puede estar vacío si no se encuentran productos.
/// </param>
public record GetProductsResult(IEnumerable<Product> Products);


/// <summary>
/// Handles queries for retrieving products from the data store.
/// Maneja consultas para recuperar productos del almacén de datos.
/// </summary>
/// <param name="session">The document session used to query the product data source.</param>
internal class GetProductsQueryHandler(
    IDocumentSession session
    ) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {        
        var products = await session.Query<Product>().ToListAsync(cancellationToken);
        
        return new GetProductsResult(products);
    }
}