using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Dtos.Transacao;
using MediatR;

namespace DespesaSimples_API.Queries.BuscarTransacaoPorIdFixaMesAno;

public class BuscarTransacaoPorIdFixaMesAnoQueryHandler(ITransacaoService transacaoService)
    : IRequestHandler<BuscarTransacaoPorIdFixaMesAnoQuery, TransacaoDto?>
{
    public async Task<TransacaoDto?> Handle(
        BuscarTransacaoPorIdFixaMesAnoQuery request,
        CancellationToken cancellationToken)
    {
        return await transacaoService.BuscarTransacaoPorIdFixaMesAnoAsync(
            request.IdTransacaoFixa,
            request.Mes,
            request.Ano);
    }
}