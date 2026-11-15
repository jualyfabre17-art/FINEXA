using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Application.Interfaces;
using Finexa.Application.Repositories;
using Finexa.Domain.Entities;
using Finexa.Domain.Exceptions;
using Finexa.Domain.Services;
using Finexa.Domain.ValueObjects;

namespace Finexa.Application.Common
{
    public sealed class RequestLoanCommandHandler : ICommandHandler<RequestLoanCommand, int>
    {
        private readonly IPartnerRepository _partnerRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ProvideALoan _loanGrantingService;
        private readonly IPartnerRepository _guarantorRepository;

        public RequestLoanCommandHandler(
            IPartnerRepository partnerRepository,
            ILoanRepository loanRepository,
            IUnitOfWork unitOfWork,
            IPartnerRepository guarantorRepository) 
        {
            _partnerRepository = partnerRepository;
            _loanRepository = loanRepository;
            _unitOfWork = unitOfWork;
            _guarantorRepository = guarantorRepository;
        }

        public async Task<int> HandleAsync(RequestLoanCommand command, CancellationToken ct = default)
        {
            var partner = await _partnerRepository.GetByIdAsync(command.PartnerId, ct);
            if (partner == null)
                throw new NotFoundException($"El socio con ID {command.PartnerId} no existe.");

            if (command.GuarantorId.HasValue)
            {
                var guarantor = await _guarantorRepository.GetByIdAsync(command.GuarantorId.Value, ct);
                if (guarantor == null)
                    throw new NotFoundException($"El garante con ID {command.GuarantorId} no existe.");
            }

            int activeLoans = await _loanRepository.CountPartnerLoanAsync(command.PartnerId, ct);

            var money = new Money(command.Amount, "DOP");

            var solicitud = new ProvideALoan(partner.Id, money, partner.AccumulatedSavings, command.GuarantorId ?? 0, command.Deadline, activeLoans, command.InterestRate);
            
            var newLoan = new Loan(partner.Id, money, command.InterestRate, command.Deadline);


            await _loanRepository.AddLoanAsync(newLoan, ct);
            await _unitOfWork.SaveChangesAsync();

            return newLoan.Id;
        }
    }
}
