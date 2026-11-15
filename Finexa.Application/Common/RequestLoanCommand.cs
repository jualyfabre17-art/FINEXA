using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Application.Interfaces;

namespace Finexa.Application.Common
{
    public sealed class RequestLoanCommand : ICommand<int>
    {
        public int PartnerId { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public int Deadline { get; set; }
        public int? GuarantorId { get; set; } 
        public DateTime StartDate { get; set; }
    }
}
