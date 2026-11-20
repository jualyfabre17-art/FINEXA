using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Finexa.Application.DTOs;
using Finexa.Application.Interfaces;
using Finexa.Application.Repositories;

namespace Finexa.Application.Queries
{
    public class GetOverdueLoansQuery : IQuery<List<OverdueLoanDto>>
    {
    }

    public class GetOverdueLoansQueryHandler : IQueryHandler<GetOverdueLoansQuery, List<OverdueLoanDto>>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IMapper _mapper;

        public GetOverdueLoansQueryHandler(
            ILoanRepository loanRepository,
            IDateTimeProvider dateTimeProvider,
            IMapper mapper)
        {
            _loanRepository = loanRepository;
            _dateTimeProvider = dateTimeProvider;
            _mapper = mapper;
        }

        public async Task<List<OverdueLoanDto>> HandleAsync(GetOverdueLoansQuery query, CancellationToken ct = default)
        {
            var today = _dateTimeProvider.UtcNow; 
            var overdueLoans = await _loanRepository.ListOverdueLoansAsync(today, ct);

            var result = new List<OverdueLoanDto>();

            foreach (var loan in overdueLoans)
            {
                var expiredInstallments = loan.Expired(today); 

                result.Add(new OverdueLoanDto
                {
                    LoanId = loan.Id,
                    PartnerId = loan.PartnerId,
                    OverdueInstallmentsCount = expiredInstallments.Count,
                    TotalOverdueAmount = expiredInstallments.Sum(i => i.Amount.Amount)
                });
            }

            return result;
        }
    }
}
