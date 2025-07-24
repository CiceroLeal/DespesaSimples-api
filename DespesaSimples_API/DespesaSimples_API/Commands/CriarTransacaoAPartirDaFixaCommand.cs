using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Dtos.Transacao;
using MediatR;

namespace DespesaSimples_API.Commands;

public record CriarTransacaoAPartirDaFixaCommand(TransacaoDto Dto) : IRequest<bool>;

public class CriarTransacaoAPartirDaFixaCommandHandler(ITransacaoService transacaoService)
    : IRequestHandler<CriarTransacaoAPartirDaFixaCommand, bool>
{
    public async Task<bool> Handle(CriarTransacaoAPartirDaFixaCommand command, CancellationToken ct)
    {
        return await transacaoService.CriarTransacaoAPartirDaFixaAsync(
            command.Dto
        );
    }
}