using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Dtos.Transacao;
using DespesaSimples_API.Entities;
using MediatR;

namespace DespesaSimples_API.Commands;

public record CriarTransacoesAPartirDaFixaCommand(
    TransacaoDto TransacaoFixaDto,
    DateTime? DataTermino,
    List<Tag> Tags) : IRequest<bool>;
    
public class CriarTransacoesAPartirDaFixaCommandHandler(ITransacaoService transacaoService)
    : IRequestHandler<CriarTransacoesAPartirDaFixaCommand, bool>
{
    public async Task<bool> Handle(CriarTransacoesAPartirDaFixaCommand command, CancellationToken ct)
    {
        return await transacaoService.CriarTransacoesAPartirDaFixaAsync(
            command.TransacaoFixaDto,
            command.DataTermino,
            command.Tags
        );
    }
}    