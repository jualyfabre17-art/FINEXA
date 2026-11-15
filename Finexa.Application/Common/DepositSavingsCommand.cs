using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Application.Interfaces;

namespace Finexa.Application.Common
{
    public sealed class DepositSavingsCommand : ICommand
    {
        public int PartnerId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
