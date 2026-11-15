using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Domain.Entities;

namespace Finexa.Application.Repositories
{
    public interface IMovementRepository
    {
        
            Task AddTransactionAsync(SavingTransaction transaction, CancellationToken ct = default);

            Task<IReadOnlyList<SavingTransaction>> GetByPartnerIdAsync(int partnerId, CancellationToken ct = default);
        
    }
}
