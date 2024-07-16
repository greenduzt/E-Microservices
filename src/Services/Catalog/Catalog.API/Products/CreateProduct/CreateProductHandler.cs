namespace Catalog.API.Products.CreateProduct;

/*
 * Init-only Properties: The properties generated from these parameters are init-only, which means they can only be set during the initialization 
 * phase and are immutable afterward. This immutability is useful for ensuring that an object does not change once it has been fully constructed, 
 * making the record thread-safe and simpler to reason about.
 * 
 * Record Keyword: Introduced in C# 9, the record keyword is used to define a reference type that provides built-in functionality for value-based 
 * equality. Records are particularly useful for immutable objects and data-carrying objects where the primary purpose is to store data with little 
 * to no behavior.
 */

// Inheriting the command object from IRequest 
/*
 * The purpose of IRequest<CreateProductResult> is to indicate that this command is a request that will be handled by MediatR and will produce a result of 
 * type CreateProductResult.
 * 
 * This means that CreateProductCommand is not just a data container but also a type of request that expects a response of type CreateProductResult.
 */
public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) 
    : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

// Accessable within the same library only
internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{       
   public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
   {
        // Create a product from the command object
        var product = new Product()
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };


        // Storing product into the session
        session.Store(product);
        // Asynchronusly saving data into the Postgres DB as a document object
        await session.SaveChangesAsync(cancellationToken);

        // Return result with the newly created product id
        return new CreateProductResult(product.Id);            
   }
}
