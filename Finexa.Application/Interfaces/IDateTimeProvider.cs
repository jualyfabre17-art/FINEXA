using System;
using System.Collections.Generic;
using System.Text;

namespace Finexa.Application.Interfaces
{
    public interface IDateTimeProvider
    {
        public DateTime UtcNow { get; }
    }
}
