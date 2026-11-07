using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Domain.Interfaces;
using Finexa.Domain.ValueObjects;

namespace Finexa.Domain.Calculations
{
    public class FrenchAmortization : IAmortization
    {
        public List<AmortizationRow> CalculateAmortization(decimal amount, decimal annualInterest, int deadline, DateTime startDate)
        {
            var table = new List<AmortizationRow>();

            decimal monthlyRate = annualInterest / 12;
            decimal outstandingBalance = amount;
            decimal fixedPayment;

            if (monthlyRate == 0)
            {
                fixedPayment = amount / deadline;
            }
            else
            {
                double rateDouble = (double)monthlyRate;
                double power = Math.Pow(1 + rateDouble, deadline);
                fixedPayment = amount * (decimal)((rateDouble * power) / (power - 1));
            }

            for (int i = 1; i <= deadline; i++)
            {
                decimal interestMonth = outstandingBalance * monthlyRate;
                decimal capitalMonth;

                if (i == deadline)
                {
                    capitalMonth = outstandingBalance;
                }
                else
                {
                    capitalMonth = fixedPayment - interestMonth;
                }

                DateTime dueDate = startDate.AddMonths(i);
                decimal roundedPrincipal = Math.Round(capitalMonth, 2);

                var row = new AmortizationRow
                {
                    Number = i,
                    DueDate = dueDate,
                    Principal = roundedPrincipal,
                    Interest = Math.Round(interestMonth, 2)
                };

                table.Add(row);

                outstandingBalance -= roundedPrincipal;
            }

            return table;
        }
    }
}
