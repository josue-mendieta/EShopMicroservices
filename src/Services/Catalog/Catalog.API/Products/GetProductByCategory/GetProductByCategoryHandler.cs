
namespace Catalog.API.Products.GetProductByCategory;

/// <summary>
/// Represents a query to retrieve products belonging to a specified category.
/// Representa una consulta para recuperar productos que pertenecen a una categoría especificada.
/// </summary>
/// <param name="Category">
/// The name of the category for which products are requested. Cannot be null or empty.
/// El nombre de la categoría para la cual se solicitan productos. No puede ser nulo o vacío.
/// </param>
public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

/// <summary>
/// Represents the result of a product query filtered by category.
/// Representa el resultado de una consulta de productos filtrada por categoría.
/// </summary>
/// <param name="Products">
/// The collection of products returned for the specified category. Cannot be null; may be empty if no products match the category.
/// La colección de productos devueltos para la categoría especificada. No puede ser nulo; puede estar vacío si no hay productos que coincidan con la categoría.
/// </param>
public record GetProductByCategoryResult(IEnumerable<Product> Products);

/// <summary>
/// Handles queries for retrieving products by category.
/// Maneja consultas para recuperar productos por categoría.
/// </summary>
/// <remarks>
/// This handler processes requests to obtain products that match a specified category. 
/// Este manejador procesa solicitudes para obtener productos que coincidan con una categoría especificada.
/// 
/// It relies on the provided document session to access product data and uses the logger for operational logging. 
/// Se basa en la sesión de documentos proporcionada para acceder a los datos de productos y utiliza el Log para el registro operativo.
/// 
/// Intended for internal use within the application's query handling infrastructure.
/// Destinado para uso interno dentro de la infraestructura de manejo de consultas de la aplicación.
/// 
/// </remarks>
/// <param name="session">The document session used to query the product data store.</param>
internal class GetProductByCategoryQueryHandler( 
    IDocumentSession session
    ) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    /// <summary>
    /// Handles a request to retrieve products that belong to the specified category.
    /// Maneja una solicitud para recuperar productos que pertenecen a la categoría especificada.
    /// </summary>
    /// <remarks>
    /// The search matches products whose category contains the specified value. 
    /// La búsqueda coincide con productos cuya categoría contiene el valor especificado.
    /// If no products are found, the result will contain an empty list.
    /// Si no se encuentran productos, el resultado contendrá una lista vacía.
    /// </remarks>
    /// <param name="query">
    /// The query containing the category criteria used to filter products. Cannot be null.
    /// La consulta que contiene los criterios de categoría utilizados para filtrar productos. No puede ser nulo.
    /// </param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A result object containing the list of products matching the specified category.</returns>
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {        
        var products = await session.Query<Product>()
            .Where(p => p.Category.Contains(query.Category))
            .ToListAsync(cancellationToken);

        return new GetProductByCategoryResult(products);
    }
}
