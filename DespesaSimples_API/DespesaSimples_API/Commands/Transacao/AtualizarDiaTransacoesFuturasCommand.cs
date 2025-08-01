using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Enums;
using MediatR;

namespace DespesaSimples_API.Commands.Transacao;

public record AtualizarDiaTransacoesFuturasCommand(TipoCategoriaEnum Tipo, int Id, int NovoDia, int AnoAtual,
    int MesAtual) :  IRequest<bool>;
    
public class AtualizarDiaTransacoesFuturasHandler(ITransacaoService transacaoService)
    : IRequestHandler<AtualizarDiaTransacoesFuturasCommand, bool>
{
    public async Task<bool> Handle(AtualizarDiaTransacoesFuturasCommand command, CancellationToken ct)
    {
        return await transacaoService.AtualizarDiaTransacoesFuturasAsync(
            command.Tipo,
            command.Id,
            command.NovoDia,
            command.AnoAtual,
            command.MesAtual
        );
    }
}    