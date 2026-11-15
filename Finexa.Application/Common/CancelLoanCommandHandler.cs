using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Application.Interfaces;
using Finexa.Application.Repositories;
using Finexa.Domain.Exceptions;

namespace Finexa.Application.Common
{
    public class CancelLoanCommandHandler : ICommandHandler<CancelLoanCommand>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelLoanCommandHandler(ILoanRepository loanRepository, IUnitOfWork unitOfWork)
        {
            _loanRepository = loanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(CancelLoanCommand command, CancellationToken ct = default)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(command.LoanId, ct);

            if (loan == null)
                throw new NotFoundException($"El préstamo con ID {command.LoanId} no existe.");

            loan.CancelLoan();

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
