using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using Finexa.Domain.Enums;
using Finexa.Domain.Exceptions;
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
        private readonly List<Guarantor> _guarantors = new();
        public IReadOnlyCollection<Guarantor> Guarantors => _guarantors.AsReadOnly();

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
            _installments.Clear();
            if (this.Method == CalculationMethod.French)
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
            else if (this.Method == CalculationMethod.German)
            {
                decimal principal = this.Amount.Amount;
                decimal capitalAmortization = principal / this.Deadline;
                decimal outstandingBalance = principal;

                for (int i = 1; i <= this.Deadline; i++)
                {
                    decimal interesPeriodo = outstandingBalance * this.InterestRate;

                    decimal montoCuota = capitalAmortization + interesPeriodo;

                    Money money = new Money(montoCuota, "DOP");
                    Installment payment = new Installment(money, i);
                    _installments.Add(payment);

                    outstandingBalance -= capitalAmortization;
                }
            }
        }

        public void AddGuarantor(Guarantor guarantor)
        {
            if (guarantor == null) throw new ArgumentNullException(nameof(guarantor), "Garante invalido");
            _guarantors.Add(guarantor);
        }

        public void RegisterPayment (decimal paymentAmount, DateTime paymentDate)
        {
            if (Status != Status.Active)
                throw new DomainException($"Prestamo {Status}: no admite pagos");

            var pendingBalance = _installments.FirstOrDefault(i => !i.IsPaid)
                ?? throw new DomainException("Sin pagos pendiente");

            if (paymentAmount < pendingBalance.Amount.Amount)
                throw new DomainException($"Amount {paymentAmount} is invalid.");
            
            pendingBalance.MarkAsPaid();

            if (_installments.All(i => i.IsPaid))
                Status = Status.Payed;
        }

        public void CancelLoan()
        {
            if (_installments.Any(i => i.IsPaid))
                throw new DomainException("No es posible cancelar, ya hay un pago realizado");
            
            Status = Status.Cancelled;
        }

        public decimal OutstandingBalance() => _installments.Where(i => !i.IsPaid).Sum(i => i.Amount.Amount);

        public IReadOnlyCollection<Installment> Expired(DateTime currentDate) =>
            _installments.Where(i => !i.IsPaid && i.DueDate < currentDate).ToList();
    }
}
