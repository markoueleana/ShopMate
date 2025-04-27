using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.ValueObjects;

public record Payment
{
    public string CardName  { get; set; } = default!;
    public string CardNumber { get; set; } = default!;
    public string CVV { get; set; } = default!;
    public string PaymentMethod { get; set; } = default!;
}
