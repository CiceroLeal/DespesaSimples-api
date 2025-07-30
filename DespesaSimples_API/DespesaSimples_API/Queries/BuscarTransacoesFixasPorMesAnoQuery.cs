using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Dtos.TransacaoFixa;
using DespesaSimples_API.Enums;
using MediatR;

namespace DespesaSimples_API.Queries;

public record BuscarTransacoesFixasPorMesAnoQuery(int Mes, int Ano, TipoTransacaoEnum? Tipo)
    : IRequest<List<TransacaoFixaDto>>;

public class BuscarTransacoesFixasPorMesAnoQueryHandler(ITransacaoFixaService transacaoFixaService)
    : IRequestHandler<BuscarTransacoesFixasPorMesAnoQuery, List<TransacaoFixaDto>>
{
    public async Task<List<TransacaoFixaDto>> Handle(
        BuscarTransacoesFixasPorMesAnoQuery request,
        CancellationToken cancellationToken)
    {
        return await transacaoFixaService.BuscarTransacoesFixasPorMesAnoAsync(
            request.Mes,
            request.Ano,
            request.Tipo);
    }
}