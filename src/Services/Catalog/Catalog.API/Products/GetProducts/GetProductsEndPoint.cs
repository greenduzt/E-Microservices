namespace Catalog.API.Products.GetProducts;

public record GetProductsResponse(IEnumerable<Product> Products);
public class GetProductsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductsQuery());

            var response = result.Adapt<GetProductsResponse>();
       
            return Results.Ok(response);    
        }).WithName("GetProducts")// Names the endpoint "CreateProduct".
          .Produces<GetProductsResponse>(StatusCodes.Status200OK) // This endpoint produces a CreateProductResponse with a status code of 201 (Created) on success.
          .ProducesProblem(StatusCodes.Status400BadRequest)// This endpoint might produce a problem response with a status code of 400 (Bad Request).
          .WithSummary("Get Products")// Adds a summary to the endpoint.
          .WithDescription("Get Products");// Adds a description to the endpoint.
    }
}
