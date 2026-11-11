using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Domain.Entities;

namespace Finexa.Application.Repositories
{
    public interface IPartnerRepository
    {
        Task<Partner?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<IReadOnlyList<Partner>> GetByIdsAsync(
            IEnumerable<int>  ids, CancellationToken ct);
        Task AddPartnerAsync (Partner partner, CancellationToken ct = default);
    }
}
