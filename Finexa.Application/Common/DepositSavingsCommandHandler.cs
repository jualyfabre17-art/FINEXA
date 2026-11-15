using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Application.Interfaces;
using Finexa.Application.Repositories;
using Finexa.Domain.Entities;
using Finexa.Domain.Enums;
using Finexa.Domain.Exceptions;
using Finexa.Domain.ValueObjects;

namespace Finexa.Application.Common
{
    public sealed class DepositSavingsCommandHandler : ICommandHandler<DepositSavingsCommand>
    {
        private readonly IPartnerRepository _partnerRepository;
        private readonly IMovementRepository _movementRepository; 
        private readonly IUnitOfWork _unitOfWork;

        public DepositSavingsCommandHandler(IPartnerRepository partnerRepository,IMovementRepository movementRepository,IUnitOfWork unitOfWork)
        {
            _partnerRepository = partnerRepository;
            _movementRepository = movementRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(DepositSavingsCommand command, CancellationToken ct = default)
        {
            var partner = await _partnerRepository.GetByIdAsync(command.PartnerId, ct);

            if (partner == null)
                throw new NotFoundException($"El socio con ID {command.PartnerId} no existe.");

            var money = new Money(command.Amount, "DOP");

            partner.Deposit(money);

            var transaction = new SavingTransaction(partner.Id, money, command.TransactionDate, TransactionType.Deposit);

            await _movementRepository.AddTransactionAsync(transaction, ct);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
