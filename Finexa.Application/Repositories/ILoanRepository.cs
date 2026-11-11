using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Domain.Entities;

namespace Finexa.Application.Repositories
{
    public interface ILoanRepository
    {
        Task<int> CountPartnerLoanAsync(int partnerId, CancellationToken ct = default);
        Task<Loan?> GetLoanByIdAsync(int id,  CancellationToken ct = default);
        Task<IReadOnlyList<Loan>> ListOverdueLoansAsync(DateTime dueDate , CancellationToken ct = default);
        Task AddLoanAsync(Loan loan,  CancellationToken ct = default);
    }
}
