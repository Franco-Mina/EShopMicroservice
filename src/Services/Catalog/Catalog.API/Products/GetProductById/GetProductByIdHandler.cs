
namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id):IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);

public class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsQueryHandler.Handle called with {@Query}", query);

        var result = await session.LoadAsync<Product>(query.Id, cancellationToken);

        return result == null ? throw new ProductNotFoundException() : new GetProductByIdResult(result);
    }
}
