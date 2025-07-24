using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Dtos.Transacao;
using MediatR;

namespace DespesaSimples_API.Commands;

public record ObterUltimaTransacaoPorIdFixaCommand(int Id) :  IRequest<TransacaoDto?>;

public class ObterUltimaTransacaoPorIdFixaCommandHandler(ITransacaoService transacaoService)
    : IRequestHandler<ObterUltimaTransacaoPorIdFixaCommand, TransacaoDto?>
{
    public async Task<TransacaoDto?> Handle(ObterUltimaTransacaoPorIdFixaCommand command, CancellationToken ct)
    {
        return await transacaoService.BuscarUltimaTransacaoPorIdFixaAsync(
            command.Id
        );
    }
}