using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ordering.Domain.DomainAbstraction;
using Ordering.Domain.Exceptions;

namespace Ordering.Domain.Entities;

public class Customer : Entity<Guid>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email {  get; set; } = default!;

    public static Customer CreateCustomer(Guid customerId, string firstName, string lastName, string email)
    {
        if(customerId == Guid.Empty) 
        {
            throw new DomainException("Customer Id is empty!");
        }

        return new Customer
        {
            Id = customerId,
            FirstName = firstName,
            LastName = lastName,
            Email = email

        };
    }
 
}
