using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string CardName  { get; set; } = default!;
    public string CardNumber { get; set; } = default!;
    public string CVV { get; set; } = default!;
    public string PaymentMethod { get; set; } = default!;

    protected Payment() { }
    private Payment(string cardName, string cardNumber, string cvv, string paymentMethod)
    {
        CardName = cardName;
        CardNumber = cardNumber;
        CVV = cvv; 
        PaymentMethod = paymentMethod;
    }

    public static Payment CreatePayment(string cardName, string cardNumber, string cvv, string paymentMethod)
    {
        return new Payment(cardName, cardNumber, cvv, paymentMethod);
    }
}
