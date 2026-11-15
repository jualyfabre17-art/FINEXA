using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Application.Repositories;
using Finexa.Domain.Entities;

namespace Finexa.Tests.Fakes
{
    public class FakeMovementRepository : IMovementRepository
    {
        public readonly List<SavingTransaction> Transactions = new();

        public Task AddTransactionAsync(SavingTransaction transaction, CancellationToken ct = default)
        {
            Transactions.Add(transaction);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyList<SavingTransaction>> GetByPartnerIdAsync(int partnerId, CancellationToken ct = default)
        {
            var result = Transactions.Where(t => t.PartnerId == partnerId).ToList();
            return Task.FromResult<IReadOnlyList<SavingTransaction>>(result);
        }
    }
}
