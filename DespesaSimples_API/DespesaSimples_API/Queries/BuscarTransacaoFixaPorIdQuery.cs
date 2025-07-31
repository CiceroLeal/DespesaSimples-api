using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Dtos.TransacaoFixa;
using MediatR;

namespace DespesaSimples_API.Queries;

public record BuscarTransacaoFixaPorIdQuery(int IdTransacaoFixa) : IRequest<TransacaoFixaDto?>;

public class BuscarTransacaoFixaPorIdQueryHandler(ITransacaoFixaService transacaoFixaService)
    : IRequestHandler<BuscarTransacaoFixaPorIdQuery, TransacaoFixaDto?>
{
    public async Task<TransacaoFixaDto?> Handle(
        BuscarTransacaoFixaPorIdQuery request,
        CancellationToken cancellationToken)
    {
        return await transacaoFixaService.BuscarTransacaoFixaPorIdAsync(request.IdTransacaoFixa);
    }
}