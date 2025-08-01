using DespesaSimples_API.Abstractions.Services;
using MediatR;

namespace DespesaSimples_API.Commands.Transacao;

public record RemoverTransacaoCommand(int Id) :  IRequest<bool>;

public class RemoverTransacaoCommandHandler(ITransacaoService transacaoService)
    : IRequestHandler<RemoverTransacaoCommand, bool>
{
    public async Task<bool> Handle(RemoverTransacaoCommand command, CancellationToken ct)
    {
        return await transacaoService.RemoverTransacaoPorIdAsync(
            command.Id
        );
    }
}