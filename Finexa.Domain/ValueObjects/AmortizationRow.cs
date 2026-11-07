using System;
using System.Collections.Generic;
using System.Text;

namespace Finexa.Domain.ValueObjects
{
    public class AmortizationRow
    {
        public int Number {  get; set; }
        public DateTime DueDate { get; set; }
        public decimal Interest { get; set; }
        public decimal Principal { get; set; }
    }
}
