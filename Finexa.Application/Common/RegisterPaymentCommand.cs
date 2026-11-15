using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Application.Interfaces;

namespace Finexa.Application.Common
{
    public sealed class RegisterPaymentCommand : ICommand
    {
        public int LoanId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
