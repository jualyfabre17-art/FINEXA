using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Domain.ValueObjects;

namespace Finexa.Domain.Entities
{
    public class Payment
    {
        public int Id { get; private set; }
        public int LoanId { get; private set; }
        public int InstallmentId { get; private set; }
        public Money PaymentAmount { get; private set; }
        public DateTime PaymentDate { get; private set; }

        private Payment () { }

        public Payment (int loanId, int installmentId, Money paymentAmount, DateTime paymentDate)
        {
            if (loanId <= 0)
                throw new ArgumentOutOfRangeException(nameof(loanId), "Id del prestamo invalido");
            if (installmentId <= 0)
                throw new ArgumentOutOfRangeException(nameof(installmentId), "Id de la cuota invalido");
            if (paymentAmount == null)
                throw new ArgumentNullException(nameof(paymentAmount), "Monto del pago invalido por nulo");
            if (paymentAmount.Amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(paymentAmount), "Monto invalido");

            LoanId = loanId;
            InstallmentId = installmentId;
            PaymentAmount = paymentAmount;
            PaymentDate = paymentDate;
        }
    }
}
