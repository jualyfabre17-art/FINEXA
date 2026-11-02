using System;
using System.Collections.Generic;
using System.Text;

namespace Finexa.Domain.ValueObjects
{
    public record Money
    {
        public decimal Amount { get; init; }
        public string Coin { get; init; } = "DOP";

        public Money(decimal amount, string coin)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Monto invalido, intete de nuevo");
            if (string.IsNullOrWhiteSpace(coin))
                throw new ArgumentException("Tipo de moneda invalido", nameof(coin));

            Amount = amount;
            Coin = coin;
        }
    }
}
