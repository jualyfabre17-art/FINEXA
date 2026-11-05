using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Domain.ValueObjects;

namespace Finexa.Domain.Services
{
    public class ProvideALoan
    {
        public int PartnerId { get; private set; }
        public Money Amount { get; private set; }
        public Money PartnerSavings { get; private set; }
        public int GuarantorId { get; private set; }
        public int Deadline { get; private set; }
        public int ActiveLoan { get; private set; }
        public decimal InterestRate { get; private set; }

        private ProvideALoan() { }

        public ProvideALoan(int partnerId, Money amount, Money partnerSavings, int guarantorId, int deadline, int activeLoan, decimal interestRate)
        {
            if (amount.Amount <= 0)
                throw new InvalidOperationException(" El monto tiene que ser mayor a 0");
            if (amount.Amount > (partnerSavings.Amount * 3))
                throw new InvalidOperationException("El socio no puede superar 3 veces el valor de sus ahorros");
            if (amount.Amount > (partnerSavings.Amount * 2) && guarantorId <= 0)
                throw new InvalidOperationException("Su prestamo dobla el monto de sus ahorros, necesita un garante");
            if (guarantorId < 0 && partnerId == guarantorId)
                throw new InvalidOperationException("El socio que pide el prestamo no puede ser su propio garante");
            if (activeLoan >= 2)
                throw new InvalidOperationException("No puede tener mas de dos prestamos activos");
            if (deadline < 1 || deadline > 60)
                throw new InvalidOperationException("El plazo de prestamo debe ser entre 1 y 60 meses");
            if (interestRate < 0 || interestRate > 0.60m)
                throw new InvalidOperationException("La tasa de interes debe estar entre 0% y 60%.");

            PartnerId = partnerId;
            Amount = amount;
            PartnerSavings = partnerSavings;
            GuarantorId = guarantorId;
            Deadline = deadline;
            ActiveLoan = activeLoan;
            InterestRate = interestRate;
        }

    }
}
