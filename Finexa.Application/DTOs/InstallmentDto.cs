using System;
using System.Collections.Generic;
using System.Text;

namespace Finexa.Application.DTOs
{
    public class InstallmentDto
    {
        public int Number { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Capital { get; set; }
        public decimal Interest { get; set; }
        public decimal TotalAmount { get; set; } 
        public bool IsPaid { get; set; }
    }
}
