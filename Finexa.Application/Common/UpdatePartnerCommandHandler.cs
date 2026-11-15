using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Application.Interfaces;
using Finexa.Application.Repositories;
using Finexa.Domain.Exceptions;

namespace Finexa.Application.Common
{
    public sealed class UpdatePartnerCommandHandler : ICommandHandler<UpdatePartnerCommand>
    {
        private readonly IPartnerRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePartnerCommandHandler(IPartnerRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(UpdatePartnerCommand command, CancellationToken ct = default)
        {
            var partner = await _repository.GetByIdAsync(command.Id, ct);

            if (partner == null)
                throw new NotFoundException($"El socio con ID {command.Id} no existe.");

            partner.UpdateDetails(command.Name, command.IdDocument);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
