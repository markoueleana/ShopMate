using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.ValueObjects;

public record Address 
{
    public string Country { get; set; } = default!;
    public string ZipCode { get; set; } = default!;
    public string City { get; set; } = default!;
}
