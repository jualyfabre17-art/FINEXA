using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Domain.ValueObjects;

namespace Finexa.Domain.Entities
{
    public class Installment
    {
        public int Id { get; private set; }
        public Money Amount { get; private set; }
        public int Number { get; private set; }

        private Installment() { }

        public Installment(Money installmentamount, int number)
        {
            
            if (installmentamount == null)
                throw new ArgumentNullException("Monto invalido", nameof(installmentamount));
            if (installmentamount.Amount <= 0)
                throw new ArgumentException("Monto invalido", nameof(installmentamount));
            if (number <= 0)
                throw new ArgumentOutOfRangeException("Numero invalido", nameof(number));

            Amount = installmentamount;
            Number = number;
        }
    }
}
