using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Application.Repositories;
using Finexa.Domain.Entities;
using Finexa.Domain.Enums;
using Finexa.Application.Interfaces;

namespace Finexa.Tests.Fakes
{
    public class FakeLoanRepository : ILoanRepository
    {
        public readonly List<Loan> Loans = new();

        public Task AddLoanAsync(Loan loan, CancellationToken ct = default)
        {
            Loans.Add(loan);
            return Task.CompletedTask;
        }

        public Task<Loan?> GetLoanByIdAsync(int id, CancellationToken ct = default)
        {
            var loan = Loans.FirstOrDefault(l => l.Id == id);
            return Task.FromResult(loan);
        }

        public Task<int> CountPartnerLoanAsync(int partnerId, CancellationToken ct = default)
        {
            var count = Loans.Count(l => l.PartnerId == partnerId && l.Status == Status.Active);
            return Task.FromResult(count);
        }

        public Task<IReadOnlyList<Loan>> ListOverdueLoansAsync(DateTime dueDate, CancellationToken ct = default)
        {
            var result = Loans.Where(l =>
                l.Installments.Any(i => !i.IsPaid && i.DueDate < dueDate)
            ).ToList();

            return Task.FromResult<IReadOnlyList<Loan>>(result);
        }

        public Task<IReadOnlyList<Loan>> GetByPartnerIdAsync(int partnerId, CancellationToken ct = default)
        {
            var result = Loans.Where(l => l.PartnerId == partnerId).ToList();
            return Task.FromResult<IReadOnlyList<Loan>>(result);
        }
    }
}
