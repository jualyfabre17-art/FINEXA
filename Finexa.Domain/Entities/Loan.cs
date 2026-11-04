using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Domain.Enums;
using Finexa.Domain.ValueObjects;

namespace Finexa.Domain.Entities
{
    public class Loan
    {
        public int PartnerId { get; private set; }
        public Money Amount { get; private set; }
        public decimal InterestRate { get; private set; }
        public int Deadline { get; private set; }
        public Status Status { get; private set; }
        public CalculationMethod Method { get; private set; }

        private readonly List<Installment> _installments = new();
        public IReadOnlyCollection<Installment> Installments => _installments.AsReadOnly();

        private Loan() { }

        public Loan(int partnerId, Money initialAmount, decimal interestRate, int deadline)
        {
            if (partnerId <= 0)
                throw new ArgumentOutOfRangeException(nameof(partnerId), "El Id del socio es invalido");
            if (initialAmount == null)
                throw new ArgumentNullException("Monto invalido", nameof(initialAmount));
            if (initialAmount.Amount <= 0)
                throw new ArgumentException("El monto inicial no puede ser negativo", nameof(initialAmount));
            if (interestRate < 0)
                throw new ArgumentOutOfRangeException("El interes no puede ser negativo", nameof(interestRate));
            if (deadline <= 0)
                throw new ArgumentOutOfRangeException(nameof(deadline), "El plazo tiene que ser mayor a O meses");

            PartnerId = partnerId;
            Amount = initialAmount;
            InterestRate = interestRate;
            Deadline = deadline;
            Status = Status.Active;
            Method = CalculationMethod.French;
        }

        public void CalculateLoan()
        {
            decimal totalInterest = this.Amount.Amount * this.InterestRate;
            decimal totalAmount = this.Amount.Amount + totalInterest;
            decimal AmountPerInstallment = totalAmount / this.Deadline;
            for (int i = 1; i <= this.Deadline; i++)
            {
                Money money = new Money(AmountPerInstallment, "DOP");
                Installment payment = new Installment(money, i);
                _installments.Add(payment);
            }
        }
    }
}
