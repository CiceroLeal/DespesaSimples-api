using DespesaSimples_API.Abstractions.Repositories;
using DespesaSimples_API.Dtos.Transacao;
using DespesaSimples_API.Exceptions;
using DespesaSimples_API.Mappers;
using MediatR;

namespace DespesaSimples_API.Commands.Transacao;

public class AtualizarTransacaoUnicaCommand(int transacaoId, TransacaoAtualizacaoDto dto) : IRequest<bool>
{
    public int TransacaoId { get; } = transacaoId;
    public TransacaoAtualizacaoDto Dto { get; } = dto;
}

public class AtualizarTransacaoUnicaCommandHandler(ITransacaoRepository repository, IMediator mediator)
    : IRequestHandler<AtualizarTransacaoUnicaCommand, bool>
{
    public async Task<bool> Handle(AtualizarTransacaoUnicaCommand request, CancellationToken cancellationToken)
    {
        var transacao = await repository.BuscarTransacaoPorIdAsync(request.TransacaoId)
                        ?? throw new NotFoundException("Transação não encontrada.");

        var tags = await mediator.Send(new BuscarAtualizarTagsCommand(request.Dto.Tags ?? []), cancellationToken);
        
        TransacaoMapper.MapAtualizacaoDtoParaEntidade(request.Dto, transacao, tags);
        
        await mediator.Send(new ReprocessarBalancosCommand(request.Dto.DataVencimento), cancellationToken);
        return await repository.AtualizarTransacaoAsync(transacao);
    }
}