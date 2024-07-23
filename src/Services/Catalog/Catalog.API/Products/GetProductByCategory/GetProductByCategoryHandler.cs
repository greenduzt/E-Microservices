﻿
namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category):IQuery<GetProductByCategoryResult>;
// Fetching products by category

public record GetProductByCategoryResult(IEnumerable<Product> Products);
internal class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger) 
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByCategoryQueryHandler. Handler called with {@Query}", query);

        var products = await session.Query<Product>().Where(x=>x.Category.Contains(query.Category)).ToListAsync(cancellationToken);

        return new GetProductByCategoryResult(products);
    }
}
