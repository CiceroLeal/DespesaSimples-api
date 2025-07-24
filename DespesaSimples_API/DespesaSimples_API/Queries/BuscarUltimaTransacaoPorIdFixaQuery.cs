using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Dtos.Transacao;
using MediatR;

namespace DespesaSimples_API.Queries;

public record BuscarUltimaTransacaoPorIdFixaQuery(int Id) :  IRequest<TransacaoDto?>;

public class ObterUltimaTransacaoPorIdFixaQueryHandler(ITransacaoService transacaoService)
    : IRequestHandler<BuscarUltimaTransacaoPorIdFixaQuery, TransacaoDto?>
{
    public async Task<TransacaoDto?> Handle(BuscarUltimaTransacaoPorIdFixaQuery query, CancellationToken ct)
    {
        return await transacaoService.BuscarUltimaTransacaoPorIdFixaAsync(
            query.Id
        );
    }
}