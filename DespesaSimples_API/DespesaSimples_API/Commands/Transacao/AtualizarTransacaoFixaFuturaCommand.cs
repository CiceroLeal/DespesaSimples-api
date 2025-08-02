using DespesaSimples_API.Abstractions.Infra;
using DespesaSimples_API.Abstractions.Repositories;
using DespesaSimples_API.Dtos.Transacao;
using DespesaSimples_API.Exceptions;
using DespesaSimples_API.Mappers;
using DespesaSimples_API.Util;
using MediatR;

namespace DespesaSimples_API.Commands.Transacao;

public class AtualizarTransacaoFixaFuturaCommand(string transacaoFixaId, TransacaoFuturaAtualizacaoDto dto) : IRequest<bool>
{
    public string TransacaoFixaId { get; } = transacaoFixaId;
    public TransacaoFuturaAtualizacaoDto Dto { get; } = dto;
}

public class AtualizarTransacaoFixaFuturaCommandHandler(
    ITransacaoRepository transacaoRepository,
    ITransacaoFixaRepository transacaoFixaRepository,
    ITransactionManager transactionManager,
    IMediator mediator)
    : IRequestHandler<AtualizarTransacaoFixaFuturaCommand, bool>
{
    public async Task<bool> Handle(AtualizarTransacaoFixaFuturaCommand request, CancellationToken cancellationToken)
    {
        await transactionManager.BeginTransactionAsync();
        try
        {
            var idInt = IdUtil.ParseIdToInt(request.TransacaoFixaId, 'F')
                        ?? throw new NotFoundException("ID da transação fixa inválido.");

            var transacaoFixa = await transacaoFixaRepository.BuscarTransacaoFixaPorIdAsync(idInt)
                                ?? throw new NotFoundException("Transação Fixa não encontrada.");

            var mes = request.Dto.DataVencimento.Month;
            var ano = request.Dto.DataVencimento.Year;

            var transacao = await transacaoRepository.BuscarTransacaoPorIdFixaMesAnoAsync(idInt, mes, ano)
                            ?? new Entities.Transacao();

            var tags = await mediator.Send(new BuscarAtualizarTagsCommand(request.Dto.Tags ?? []), cancellationToken);

            TransacaoMapper.MapFuturaAtualizacaoDtoParaEntidade(request.Dto, transacaoFixa, transacao, tags);

            if (transacao.IdTransacao == 0)
                await transacaoRepository.CriarTransacaoAsync(transacao);
            else
                await transacaoRepository.AtualizarTransacaoAsync(transacao);

            await mediator.Send(new ReprocessarBalancosCommand(request.Dto.DataVencimento), cancellationToken);

            await transactionManager.CommitAsync();
            return true;
        }
        catch
        {
            await transactionManager.RollbackAsync();
            throw;
        }
    }
}