using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

/*
 * This is an extension method provided by the MediatR library to register MediatR services with the 
 * dependency injection (DI) container in a .NET application, which will manage our command and query 
 * handlers into the builder services
 * Configuring the mediator using the config method
 */
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config => {
    /*
     * This method tells MediatR to scan the specified assembly for handlers, requests, 
     * and other MediatR-related services.
     * typeof(Program).Assembly specifies that the assembly to scan is the one where the Program class is defined. 
     * This is typically the main assembly of your application.
     */
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviors<,>));
});

// Fluent Validation
builder.Services.AddValidatorsFromAssembly(assembly);
/*
 * This registers the Carter classes into ASP.Net Dependency Injection container.
 * The AddCarter method, adds the necessary services for ASP.Net dependency injection.
 */
builder.Services.AddCarter();

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database"));
    
}).UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP request pipeline
/*
 * After that configure Http request pipeline with Carter in order to expose the Http get and post methods.
 * The MapCarter() method maps, the routes defined in the ICarter module implementation
 */
app.MapCarter();

app.Run();
