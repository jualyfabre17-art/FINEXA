using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Domain.Interfaces;
using Finexa.Domain.ValueObjects;

namespace Finexa.Domain.Calculations
{
    public class GermanAmortization : IAmortization
    {
        public List<AmortizationRow> CalculateAmortization(decimal amount, decimal annualInterest, int deadline, DateTime startDate)
        {
            decimal fixedPayment = amount / deadline;
            decimal monthlyRate = annualInterest / 12; 
            decimal outstandingBalance = amount;

            var table = new List<AmortizationRow>();

            for (int i = 1; i <= deadline; i++)
            {
                decimal interestMonth = outstandingBalance * monthlyRate;

                decimal monthPrincipal;
                if (i == deadline)
                {
                    monthPrincipal = outstandingBalance;
                }
                else
                {
                    monthPrincipal = fixedPayment;
                }

                DateTime dueDate = startDate.AddMonths(i);
                decimal roundedPrincipal = Math.Round(monthPrincipal, 2);

                var row = new AmortizationRow
                {
                    Number = i,
                    DueDate = dueDate,
                    Principal = roundedPrincipal, 
                    Interest = Math.Round(monthlyRate, 2)
                };

                table.Add(row);

                outstandingBalance -= roundedPrincipal;
            }

            return table;
        }
    }
}
