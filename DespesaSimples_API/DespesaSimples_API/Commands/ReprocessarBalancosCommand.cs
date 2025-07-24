using DespesaSimples_API.Abstractions.Services;
using MediatR;

namespace DespesaSimples_API.Commands;

public record ReprocessarBalancosCommand(DateTime DataAlterada) : IRequest<bool>;

public class ReprocessarBalancosCommandHandler(IBalancoService balancoService)
    : IRequestHandler<ReprocessarBalancosCommand, bool>
{
    public async Task<bool> Handle(ReprocessarBalancosCommand command, CancellationToken ct)
    {
        return await balancoService.ReprocessarBalancosAPartirDeAsync(command.DataAlterada);
    }
}