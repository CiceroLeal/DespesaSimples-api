using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Dtos.Transacao;
using DespesaSimples_API.Dtos.TransacaoFixa;
using DespesaSimples_API.Entities;
using MediatR;

namespace DespesaSimples_API.Commands.TransacaoFixa;

public record CriarTransacaoFixaAPartirDoCriacaoDtoCommand(TransacaoCriacaoDto? TransacaoDto, List<Tag> Tags) : IRequest<TransacaoFixaDto?>;

public class CriarTransacaoFixaAPartirDoCriacaoDtoCommandHandler(ITransacaoFixaService transacaoFixaService)
    : IRequestHandler<CriarTransacaoFixaAPartirDoCriacaoDtoCommand, TransacaoFixaDto?>
{
    public async Task<TransacaoFixaDto?> Handle(CriarTransacaoFixaAPartirDoCriacaoDtoCommand command, CancellationToken ct)
    {
        return await transacaoFixaService
            .CriarTransacaoFixaAPartirDoCriacaoDtoAsync(command.TransacaoDto, command.Tags);
    }
}