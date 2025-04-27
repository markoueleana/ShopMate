using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Domain.DomainAbstraction;

namespace Order.Domain.Entities;

public class Customer : Entity<Guid>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email {  get; set; } = default!;
}
