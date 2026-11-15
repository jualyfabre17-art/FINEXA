using System;
using System.Collections.Generic;
using System.Text;

namespace Finexa.Application.Interfaces
{
    public interface ICommand<TResult> { }

    public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
        Task<TResult> HandleAsync(TCommand command, CancellationToken ct = default);
    }
}
