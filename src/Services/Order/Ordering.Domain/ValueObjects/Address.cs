using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects;

public record Address 
{
    public string Country { get; set; } = default!;
    public string ZipCode { get; set; } = default!;
    public string City { get; set; } = default!;

    protected Address() { }

    private Address(string country, string zipCode, string city)
    { 
        Country = country;
        ZipCode = zipCode;
        City = city;
    }
    public static Address CreateAddress(string country, string zipCode, string city)
    { 
        return new Address(country, zipCode, city);
    }
}
