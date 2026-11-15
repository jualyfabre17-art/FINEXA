using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Application.Repositories;
using Finexa.Domain.Entities;

namespace Finexa.Tests.Fakes
{
    public class FakePaymentRepository : IPaymentRepository
    {
        public readonly List<Payment> Payments = new();

        public Task AddPaymentAsync(Payment payment, CancellationToken ct = default)
        {
            Payments.Add(payment);
            return Task.CompletedTask;
        }
    }
}
