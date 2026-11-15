using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Application.Interfaces;
using Finexa.Application.Repositories;
using Finexa.Domain.Entities;
using Finexa.Domain.ValueObjects;

namespace Finexa.Application.Common
{
    public sealed class CreatePartnerCommandHandler : ICommandHandler<CreatePartnerCommand, int>
    {
        private readonly IPartnerRepository _partnerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePartnerCommandHandler(
            IPartnerRepository partnerRepository,
            IUnitOfWork unitOfWork)
        {
            _partnerRepository = partnerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> HandleAsync(CreatePartnerCommand command, CancellationToken ct = default)
        {
            var initialMoney = new Money(command.InitialSavingsAmount, "DOP");

            var newPartner = new Partner(command.Name, command.IdDocument, initialMoney);

            await _partnerRepository.AddPartnerAsync(newPartner, ct);

            await _unitOfWork.SaveChangesAsync();

            return newPartner.Id;
        }
    }
}
