using System;
using System.Collections.Generic;
using System.Text;
using Finexa.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Finexa.Application.Common
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResult> SendAsync<TCommand, TResult>(TCommand command, CancellationToken ct = default)
            where TCommand : ICommand<TResult>
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
            return await handler.HandleAsync(command, ct);
        }

        public async Task SendAsync<TCommand>(TCommand command, CancellationToken ct = default)
            where TCommand : ICommand
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            await handler.HandleAsync(command, ct);
        }
    }
}
