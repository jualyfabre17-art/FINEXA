using System;
using System.Collections.Generic;
using System.Text;


namespace Finexa.Application.Interfaces
{
    public interface IMediator
    {
        Task<TResult> SendAsync<TCommand, TResult>(TCommand command, CancellationToken ct = default)
            where TCommand : ICommand<TResult>;

        Task SendAsync<TCommand>(TCommand command, CancellationToken ct = default)
            where TCommand : ICommand;
    }

}
