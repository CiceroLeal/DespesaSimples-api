using DespesaSimples_API.Abstractions.Services;
using MediatR;

namespace DespesaSimples_API.Commands;

public record RemoverTransacoesPorIdTransacaoFixaCommand(int Id) :  IRequest<bool>;

public class RemoverTransacoesPorIdTransacaoFixaCommandHandler(ITransacaoService transacaoService)
    : IRequestHandler<RemoverTransacoesPorIdTransacaoFixaCommand, bool>
{
    public async Task<bool> Handle(RemoverTransacoesPorIdTransacaoFixaCommand command, CancellationToken ct)
    {
        return await transacaoService.RemoverTransacoesPorIdTransacaoFixaAsync(
            command.Id
        );
    }
}