using System;
using System.Collections.Generic;
using System.Text;

namespace Finexa.Application.DTOs
{
    public class LoanDetailDto
    {
        public int Id { get; set; }
        public int PartnerId { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public int Deadline { get; set; }
        public string Status { get; set; } = string.Empty; 
        public decimal PendingBalance { get; set; }

        public List<InstallmentDto> Installments { get; set; } = new();
    }
}
