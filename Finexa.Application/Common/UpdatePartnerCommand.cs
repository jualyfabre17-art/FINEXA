using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Application.Interfaces;

namespace Finexa.Application.Common
{
    public sealed class UpdatePartnerCommand : ICommand
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public int IdDocument { get; set; }
    }
}
