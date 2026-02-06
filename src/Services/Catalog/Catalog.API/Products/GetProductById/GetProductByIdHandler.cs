namespace Catalog.API.Products.GetProductById;


public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product Product);

internal class GetProductByIdQueryHandler(
    ILogger<GetProductByIdQueryHandler> logger,
    IDocumentSession session
    ) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByIdHandler.Handle called with {@Query}", query);
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if (product == null)
        {
            throw new ProductNotFoundException($"Product with id {query.Id} not found");
        }

        return new GetProductByIdResult(product);
    }
}
