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

        Task<TResult> SendQueryAsync<TQuery, TResult>(TQuery query, CancellationToken ct = default)
            where TQuery : IQuery<TResult>;
    }

}
