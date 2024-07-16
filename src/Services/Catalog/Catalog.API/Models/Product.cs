namespace Catalog.API.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        // Gets the default value of a type.
        // For reference types (like string), the default value is null.
        // Null-forgiving Operator (!)
        public string Name { get; set; } = default!;
       
        /* The effect of this initialization is that whenever an 
         * instance of the class containing this property is created, 
         * Category will automatically be initialized to a new, empty 
         * list of strings. This prevents the property from being null 
         * and avoids NullReferenceException errors when you attempt to 
         * add items to the list or manipulate it in other ways without 
         * explicitly initializing it elsewhere.
         * 
         * Product can have many categories 1 to N
         */
        public List<string> Category { get; set; } = new();

        public string Description { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public decimal Price { get; set; }
    }
}
