using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Application.Interfaces;

namespace Finexa.Tests.Fakes
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        public Task<int> SaveChangesAsync()
        {
            return Task.FromResult(1);
        }
    }
}
