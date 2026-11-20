using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Finexa.Application.DTOs;
using Finexa.Domain.Entities;

namespace Finexa.Application.Mappings
{
    public class LoanMappingProfile : Profile
    {
        public LoanMappingProfile()
        {
            CreateMap<Installment, InstallmentDto>()
                .ForMember(dest => dest.Capital, opt => opt.MapFrom(src => src.Amount.Amount)) 
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Amount.Amount))
                .ForMember(dest => dest.IsPaid, opt => opt.MapFrom(src => src.IsPaid));

            CreateMap<Loan, LoanDetailDto>()
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount.Amount))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.PendingBalance, opt => opt.MapFrom(src => src.OutstandingBalance()))
                .ForMember(dest => dest.Installments, opt => opt.MapFrom(src => src.Installments));
        }
    }
}
