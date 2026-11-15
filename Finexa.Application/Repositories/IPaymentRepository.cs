using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Domain.Entities;

namespace Finexa.Application.Repositories
{
    public interface IPaymentRepository
    {
        Task AddPaymentAsync(Payment payment, CancellationToken ct = default);
    }
}
