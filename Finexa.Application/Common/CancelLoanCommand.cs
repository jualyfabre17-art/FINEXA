using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Application.Interfaces;

namespace Finexa.Application.Common
{
    public sealed class CancelLoanCommand : ICommand
    {
        public int LoanId { get; set; }
    }
}
