using DespesaSimples_API.Abstractions.Services;
using MediatR;

namespace DespesaSimples_API.Queries.SomarTransacoesPorTipo;

public class SomarTransacoesPorTipoQueryHandler(ITransacaoService transacaoService)
    : IRequestHandler<SomarTransacoesPorTipoQuery, decimal>
{
    public async Task<decimal> Handle(
        SomarTransacoesPorTipoQuery request,
        CancellationToken cancellationToken)
    {
        return await transacaoService.SomarPorTipoAsync(
            request.TipoTransacao,
            request.Ano,
            request.Mes);
    }
}