using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Dtos.Transacao;
using DespesaSimples_API.Entities;
using MediatR;

namespace DespesaSimples_API.Commands.Transacao;

public record AtualizarTransacaoAPartirDaFixaCommand(int Id, TransacaoDto Dto, List<Tag> Tags) :  IRequest<bool>;

public class AtualizarTransacaoAPartirDaFixaCommandHandler(ITransacaoService transacaoService)
    : IRequestHandler<AtualizarTransacaoAPartirDaFixaCommand, bool>
{
    public async Task<bool> Handle(AtualizarTransacaoAPartirDaFixaCommand command, CancellationToken ct)
    {
        return await transacaoService.AtualizarTransacaoAPartirDaFixaAsync(
            command.Id,
            command.Dto,
            command.Tags
        );
    }
}