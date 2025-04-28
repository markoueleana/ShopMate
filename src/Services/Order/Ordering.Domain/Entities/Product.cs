using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ordering.Domain.DomainAbstraction;

namespace Ordering.Domain.Entities;

public class Product : Entity<Guid>
{
    public string Name { get; set; } = default!;
    public decimal Price { get; set; } = default!;

    public static Product CreateProduct(Guid productId, string name, decimal price)
    {
        return new Product()
        {
            Id = productId,
            Name = name,
            Price = price,

        };
    }
}
