namespace Catalog.API.Products.GetProducts;
/*
 * CQRS pattern
 */

/*
 * GetProductsQuery(): This is a parameterless record. It represents a query with no parameters.
 * Query will return a GetProductsResult
 */
public record GetProductsQuery(): IQuery<GetProductsResult>;

/*
 * Result of the GetProductsQuery() 
 */
public record GetProductsResult(IEnumerable<Product> Products);

/*
 * The constructor takes two dependencies: IDocumentSession and ILogger<GetProductsQueryHandler>.
 */
internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger) 
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    /*
     * This method handles the GetProductsQuery.
     * query: The GetProductsQuery instance.
     * cancellationToken: A token to signal cancellation of the query.
     */
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsQueryHandler. Handler called with {@Query}",query);
        
        // Asynchronously queries the document session for Product entities and converts the result to a list.
        var products = await session.Query<Product>().ToListAsync(cancellationToken);
        
        //Returns a new GetProductsResult containing the list of products.
        return new GetProductsResult(products);
    }
}
