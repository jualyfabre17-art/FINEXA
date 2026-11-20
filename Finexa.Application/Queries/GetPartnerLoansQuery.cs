using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Finexa.Application.DTOs;
using Finexa.Application.Interfaces;
using Finexa.Application.Repositories;

namespace Finexa.Application.Queries
{
    public class GetPartnerLoansQuery : IQuery<List<LoanDetailDto>>
    {
        public int PartnerId { get; set; }
    }

    public class GetPartnerLoansQueryHandler : IQueryHandler<GetPartnerLoansQuery, List<LoanDetailDto>>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;

        public GetPartnerLoansQueryHandler(ILoanRepository loanRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        public async Task<List<LoanDetailDto>> HandleAsync(GetPartnerLoansQuery query, CancellationToken ct = default)
        {
            var loans = await _loanRepository.GetByPartnerIdAsync(query.PartnerId, ct);

            return _mapper.Map<List<LoanDetailDto>>(loans);
        }
    }
}
