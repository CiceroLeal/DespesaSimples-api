using DespesaSimples_API.Abstractions.Services;
using MediatR;

namespace DespesaSimples_API.Commands.AtualizarDiaTransacoesFuturas;

public class ReprocessarBalancosCommandHandler(ITransacaoService transacaoService)
    : IRequestHandler<AtualizarDiaTransacoesFuturasCommand, bool>
{
    public async Task<bool> Handle(AtualizarDiaTransacoesFuturasCommand notification, CancellationToken ct)
    {
        return await transacaoService.AtualizarDiaTransacoesFuturasAsync(
            notification.Tipo,
            notification.Id,
            notification.NovoDia,
            notification.AnoAtual,
            notification.MesAtual
        );
    }
}