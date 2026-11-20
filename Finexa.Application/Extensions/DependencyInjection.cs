using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Application.Common;
using Finexa.Application.DTOs;
using Finexa.Application.Interfaces;
using Finexa.Application.Mappings;
using Finexa.Application.Queries;
using Microsoft.Extensions.DependencyInjection;
using Finexa.Application.Handlers;

namespace Finexa.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<LoanMappingProfile>();
            });

            services.AddScoped<IMediator, Mediator>();

            services.AddScoped<ICommandHandler<CreatePartnerCommand, int>, CreatePartnerCommandHandler>();
            
            services.AddScoped<IQueryHandler<GetLoanDetailQuery, LoanDetailDto>, GetLoanDetailQueryHandler>();
            services.AddScoped<IQueryHandler<GetPartnerLoansQuery, List<LoanDetailDto>>, GetPartnerLoansQueryHandler>();
            services.AddScoped<IQueryHandler<GetOverdueLoansQuery, List<OverdueLoanDto>>, GetOverdueLoansQueryHandler>();

            return services;
        }
    }
}
