using System;
using System.Collections.Generic;
using System.Text;

namespace Finexa.Application.DTOs
{
    public class OverdueLoanDto
    {
        public int LoanId { get; set; }
        public int PartnerId { get; set; }
        public string PartnerName { get; set; } = string.Empty; // Opcional, si el repo lo trae
        public int OverdueInstallmentsCount { get; set; }
        public decimal TotalOverdueAmount { get; set; }
    }
}
