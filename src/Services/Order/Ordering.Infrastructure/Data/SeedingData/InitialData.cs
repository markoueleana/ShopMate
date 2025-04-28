using Ordering.Domain.Entities;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Extensions;
internal class InitialData
{
    public static IEnumerable<Customer> Customers =>
    new List<Customer>
    {
        Customer.CreateCustomer(new Guid("58c49479-ec65-4de2-86e7-033c546291aa"), "Eleana","Markou", "eleana@gmail.com"),
        Customer.CreateCustomer(new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d"), "John","douglas", "john@gmail.com")
    };
    public static IEnumerable<Product> Products =>
        new List<Product>
        {
            Product.CreateProduct(new Guid("f2a1e4fc-1b75-4b29-912f-3ef567b421a1"), "Wireless Mouse", 29.99M),
            Product.CreateProduct(new Guid("c3e4520a-21aa-4e3a-b7cc-6f3654f6e320"), "Gaming Keyboard", 89.99M),
            Product.CreateProduct(new Guid("d02d8b4e-7791-4d4f-87dc-4a765b3a8f13"), "Running Shoes", 119.99M),
            Product.CreateProduct(new Guid("5b438e9b-f92c-44b1-bc02-5c6e11e2290e"), "Smartwatch", 199.99M)
        };

  
}
