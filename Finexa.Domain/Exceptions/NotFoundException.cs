using System;
using System.Collections.Generic;
using System.Text;

namespace Finexa.Domain.Exceptions
{
    public sealed class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message){}
    }
}
