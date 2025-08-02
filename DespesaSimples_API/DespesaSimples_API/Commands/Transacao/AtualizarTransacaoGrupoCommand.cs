using DespesaSimples_API.Abstractions.Repositories;
using DespesaSimples_API.Dtos.Transacao;
using DespesaSimples_API.Exceptions;
using DespesaSimples_API.Mappers;
using MediatR;

namespace DespesaSimples_API.Commands.Transacao;

public class AtualizarTransacaoGrupoCommand(string grupoParcelasId, TransacaoAtualizacaoDto dto) : IRequest<bool>
{
    public string GrupoParcelasId { get; } = grupoParcelasId;
    public TransacaoAtualizacaoDto Dto { get; } = dto;
}

public class AtualizarTransacaoGrupoCommandHandler(ITransacaoRepository repository, IMediator mediator)
    : IRequestHandler<AtualizarTransacaoGrupoCommand, bool>
{
    public async Task<bool> Handle(AtualizarTransacaoGrupoCommand request, CancellationToken cancellationToken)
    {
        var transacoes = await repository.BuscarTransacaoPorGrpParcelaIdAsync(request.GrupoParcelasId);
        if (transacoes.Count == 0)
            throw new NotFoundException("Nenhuma transação encontrada para o grupo de parcelas.");

        var tags = await mediator.Send(new BuscarAtualizarTagsCommand(request.Dto.Tags ?? []), cancellationToken);

        foreach (var transacao in transacoes)
        {
            TransacaoMapper.MapAtualizacaoDtoParaEntidade(request.Dto, transacao, tags);
        }

        var result = await repository.SaveChangesAsync();

        if (!result)
            return false;

        await mediator.Send(new ReprocessarBalancosCommand(request.Dto.DataVencimento), cancellationToken);

        return true;
    }
}