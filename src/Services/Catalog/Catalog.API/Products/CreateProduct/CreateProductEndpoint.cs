namespace Catalog.API.Products.CreateProduct;

// A record to encapsulate the data needed to create a product.
public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
// A record to encapsulate the response after creating a product.
public record CreateProductResponse(Guid Id);
/*
 * END POINT CLASS
 * once we get the request object, we should convert this request object as a command object in order to trigger command handler.
   So how we can convert request to command object is by using a mapping library called mapster. Mapster is a high performance object mapper.
   And it is very straightforward and flexible and perfect for mapping objects. We use mapster in order to convert our request module to the 
   mediator command object and vice versa.
 */
public class CreateProductEndpoint : ICarterModule
{
    /*
     * This function we will define our Http post endpoint using Carter and Mapster.After that we map our request to a command object. And we 
     * send it through the mediator and then map the result back to the response model.
     */
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        /* app.Map operation mapping the http post request.
         * For that purpose, using the MapPost method, and in the configuration of this method we should define the request Uri as products, 
         * and in the second parameter, we implement the actual handler method of this request. For that purpose, we can define an async 
         * anonymous function to get a CreateProductRequest parameter from the request body and will inject ISender object from the mediator.
         */
        //          request URI  Handler method of the request
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) => 
        {
            /* 
             * Uses Mapster to map the CreateProductRequest to a CreateProductCommand. This implies that there is a CreateProductCommand class 
             * that represents the command to create a product.            
             */
            var command = request.Adapt<CreateProductCommand>();

            /*
             * Uses MediatR's ISender to send the CreateProductCommand and get the result. This implies that there is a handler for CreateProductCommand 
             * which processes the command and returns a result.
             */
            var result = await sender.Send(command);

            /*
             * And after getting the result, we can again convert the response type from the result. Using the Mapster adapt method, we can 
             * convert back to our create product response object and we can return results that created.
             */
            var response = result.Adapt<CreateProductResponse>();
            /*
             * Returns a 201 Created response with the location of the created product and the CreateProductResponse object.
             */
            return Results.Created($"/products/{response.Id}",response);

            // Additional Endpoint Configuration
        }).WithName("CreateProduct")// Names the endpoint "CreateProduct".
          .Produces<CreateProductResponse>(StatusCodes.Status201Created) // This endpoint produces a CreateProductResponse with a status code of 201 (Created) on success.
          .ProducesProblem(StatusCodes.Status400BadRequest)// This endpoint might produce a problem response with a status code of 400 (Bad Request).
          .WithSummary("CreateProduct")// Adds a summary to the endpoint.
          .WithDescription("CreateProduct");// Adds a description to the endpoint.
    }
}

