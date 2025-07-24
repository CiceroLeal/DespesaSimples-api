using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Dtos.Transacao;
using DespesaSimples_API.Entities;
using MediatR;

namespace DespesaSimples_API.Commands;

public record AtualizarTransacoesAPartirDaFixaCommand(
    int IdTransacaoFixa,
    TransacaoDto TransacaoDto,
    List<Tag> Tags) : IRequest<bool>;
    
public class AtualizarTransacoesAPartirDaFixaCommandHandler(ITransacaoService transacaoService)
    : IRequestHandler<AtualizarTransacoesAPartirDaFixaCommand, bool>
{
    public async Task<bool> Handle(AtualizarTransacoesAPartirDaFixaCommand command, CancellationToken ct)
    {
        return await transacaoService.AtualizarTransacoesAPartirDaFixaAsync(
            command.IdTransacaoFixa,
            command.TransacaoDto,
            command.Tags
        );
    }
}    