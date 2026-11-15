using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Application.Interfaces;
namespace Finexa.Application.Common
{
    

    namespace Finexa.Application.Common
    {
        public class CreatePartnerCommand : ICommand<int>
        {
            public string Name { get; set; } = string.Empty;
            public int IdDocument { get; set; }
            public decimal InitialSavingsAmount { get; set; } 
        }
    }
}
