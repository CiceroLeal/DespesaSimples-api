using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Dtos.Transacao;
using MediatR;

namespace DespesaSimples_API.Commands;

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