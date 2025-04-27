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
    public string Expiration { get; set; } = default!;

    protected Payment() { }
    private Payment(string cardName, string cardNumber, string cvv, string paymentMethod, string expiration)
    {
        CardName = cardName;
        CardNumber = cardNumber;
        CVV = cvv; 
        PaymentMethod = paymentMethod;
        Expiration = expiration;
    }

    public static Payment CreatePayment(string cardName, string cardNumber, string cvv, string paymentMethod,string expiration)
    {
        return new Payment(cardName, cardNumber, cvv, paymentMethod, expiration);
    }
}
