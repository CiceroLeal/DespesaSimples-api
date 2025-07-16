using DespesaSimples_API.Abstractions.Services;
using MediatR;

namespace DespesaSimples_API.Commands.ReprocessarBalancos;

public class ReprocessarBalancosCommandHandler(IMediator mediator, IBalancoService balancoService)
    : INotificationHandler<ReprocessarBalancosCommand>
{
    public async Task Handle(ReprocessarBalancosCommand notification, CancellationToken ct)
    {
        await mediator.Send(balancoService.ReprocessarBalancosAPartirDeAsync(notification.DataAlterada), ct);
    }
}