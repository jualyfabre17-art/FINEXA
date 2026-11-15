using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Application.Repositories;
using Finexa.Domain.Entities;

namespace Finexa.Tests.Fakes
{
    public class FakePartnerRepository : IPartnerRepository
    {
        public readonly List<Partner> Partners = new();

        public Task AddPartnerAsync(Partner partner, CancellationToken ct = default)
        {
            Partners.Add(partner);
            return Task.CompletedTask;
        }

        public Task<Partner?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var partner = Partners.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(partner);
        }

        public Task<IReadOnlyList<Partner>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken ct = default)
        {
            var result = Partners.Where(p => ids.Contains(p.Id)).ToList();
            return Task.FromResult<IReadOnlyList<Partner>>(result);
        }
    }
}
