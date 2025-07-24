using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Enums;
using MediatR;

namespace DespesaSimples_API.Queries;

public record SomarTransacoesPorTipoQuery(
    TipoTransacaoEnum TipoTransacao,
    int Ano,
    int Mes
) : IRequest<decimal>;

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