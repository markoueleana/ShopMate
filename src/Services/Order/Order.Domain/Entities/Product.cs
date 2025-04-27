using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Domain.DomainAbstraction;

namespace Order.Domain.Entities;

public class Product : Entity<Guid>
{
    public string Name { get; set; } = default!;
    public decimal Price { get; set; } = default!;
}
