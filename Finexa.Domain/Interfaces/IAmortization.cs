using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Domain.ValueObjects;

namespace Finexa.Domain.Interfaces
{
    public interface IAmortization
    {
        List<AmortizationRow> CalculateAmortization(decimal amount, decimal annualInterest, int deadline, DateTime startDate);
        
    }
}
