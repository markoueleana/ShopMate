using Catalog.API.Entities;
using Marten;
using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync()) return;

        session.Store<Product>(GetConfiguredProducts());
        await session.SaveChangesAsync();
    }

    private static IEnumerable<Product> GetConfiguredProducts() => new List<Product>()
    {
        new Product
    {
        Id = Guid.Parse("f2a1e4fc-1b75-4b29-912f-3ef567b421a1"),
        Name = "Wireless Mouse",
        Description = "A smooth and responsive wireless mouse with ergonomic design.",
        Category = new List<string> { "Electronics", "Accessories" },
        Price = 29.99M
    },
    new Product
    {
        Id = Guid.Parse("c3e4520a-21aa-4e3a-b7cc-6f3654f6e320"),
        Name = "Gaming Keyboard",
        Description = "Mechanical keyboard with RGB lighting for an enhanced gaming experience.",
        Category = new List<string> { "Electronics", "Gaming" },
        Price = 89.99M
    },
    new Product
    {
        Id = Guid.Parse("d02d8b4e-7791-4d4f-87dc-4a765b3a8f13"),
        Name = "Running Shoes",
        Description = "Lightweight running shoes designed for maximum comfort and performance.",
        Category = new List<string> { "Footwear", "Sports" },
        Price = 119.99M
    },
    new Product
    {
        Id = Guid.Parse("5b438e9b-f92c-44b1-bc02-5c6e11e2290e"),
        Name = "Smartwatch",
        Description = "Water-resistant smartwatch with heart rate monitor and GPS tracking.",
        Category = new List<string> { "Electronics", "Wearables" },
        Price = 199.99M
    },
    new Product
    {
        Id = Guid.Parse("e7209f4e-827e-465b-897b-91e3e9a00b7f"),
        Name = "Bluetooth Speaker",
        Description = "Portable Bluetooth speaker with deep bass and 12-hour battery life.",
        Category = new List<string> { "Electronics", "Audio" },
        Price = 59.99M
    },
    new Product
    {
        Id = Guid.Parse("b5d50c95-7824-4ad7-8a5f-7c1129d8b10a"),
        Name = "Coffee Maker",
        Description = "Programmable coffee maker with built-in grinder and timer.",
        Category = new List<string> { "Home Appliances", "Kitchen" },
        Price = 149.99M
    },
    new Product
    {
        Id = Guid.Parse("94e09779-4473-4c69-8c26-12b4ccf04f1d"),
        Name = "Office Chair",
        Description = "Ergonomic office chair with lumbar support and adjustable height.",
        Category = new List<string> { "Furniture", "Office" },
        Price = 229.99M
    },
    new Product
    {
        Id = Guid.Parse("18e64a7c-8b84-4d0f-b3cf-671f1ce1c35a"),
        Name = "Backpack",
        Description = "Durable waterproof backpack perfect for travel or school.",
        Category = new List<string> { "Accessories", "Travel" },
        Price = 69.99M
    },
    new Product
    {
        Id = Guid.Parse("80a7086e-cf41-4c48-84d8-b353c8f2c28e"),
        Name = "LED Monitor",
        Description = "27-inch Full HD LED monitor with ultra-thin bezels.",
        Category = new List<string> { "Electronics", "Computers" },
        Price = 179.99M
    },
    new Product
    {
        Id = Guid.Parse("3498a2f8-6b41-4b91-85c9-2b2d0c7fc9e1"),
        Name = "Electric Toothbrush",
        Description = "Rechargeable electric toothbrush with 5 brushing modes.",
        Category = new List<string> { "Health", "Personal Care" },
        Price = 49.99M
    }
    }; 
}
