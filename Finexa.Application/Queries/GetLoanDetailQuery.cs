using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Finexa.Application.DTOs;
using Finexa.Application.Interfaces;
using Finexa.Application.Repositories;
using Finexa.Domain.Exceptions;

namespace Finexa.Application.Queries
{
    public class GetLoanDetailQuery : IQuery<LoanDetailDto>
    {
        public int LoanId { get; set; }
    }

    public class GetLoanDetailQueryHandler : IQueryHandler<GetLoanDetailQuery, LoanDetailDto>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;

        public GetLoanDetailQueryHandler(ILoanRepository loanRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        public async Task<LoanDetailDto> HandleAsync(GetLoanDetailQuery query, CancellationToken ct = default)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(query.LoanId, ct);

            if (loan == null)
                throw new NotFoundException($"El préstamo con ID {query.LoanId} no existe.");

            return _mapper.Map<LoanDetailDto>(loan);
        }
    }
}
