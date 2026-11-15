using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Application.Interfaces;
using Finexa.Application.Repositories;
using Finexa.Domain.Exceptions;
using Finexa.Domain.ValueObjects;
using Finexa.Domain.Entities;

namespace Finexa.Application.Common
{
    public sealed class RegisterPaymentCommandHandler : ICommandHandler<RegisterPaymentCommand>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IPaymentRepository _paymentRepository; 
        private readonly IUnitOfWork _unitOfWork;

        public RegisterPaymentCommandHandler(
            ILoanRepository loanRepository,
            IPaymentRepository paymentRepository,
            IUnitOfWork unitOfWork)
        {
            _loanRepository = loanRepository;
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(RegisterPaymentCommand command, CancellationToken ct = default)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(command.LoanId, ct);

            if (loan == null)
                throw new NotFoundException($"El prestamo con ID {command.LoanId} no existe.");

            var money = new Money(command.Amount, "DOP");

            var pendingInstallment = loan.Installments.FirstOrDefault(i => !i.IsPaid);

            if (pendingInstallment == null)
                throw new DomainException("El prestamo no tiene cuotas pendientes.");

            loan.RegisterPayment(command.Amount, command.PaymentDate);

            var paymentRecord = new Payment(loan.Id, pendingInstallment.Id, money, command.PaymentDate);

            await _paymentRepository.AddPaymentAsync(paymentRecord, ct);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
