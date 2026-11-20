using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Application.Interfaces;

namespace Finexa.Tests.Fakes
{
    public class FakeDateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
