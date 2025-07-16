using DespesaSimples_API.Dtos.TransacaoFixa;

namespace DespesaSimples_API.Abstractions.Services;

public interface ITransacaoFixaService
{
    Task<List<TransacaoFixaDto>> ObterTransacoesFixasAsync();
    Task<TransacaoFixaDto?> ObterTransacaoFixaPorIdAsync(int id);
    Task<bool> RemoverTransacaoFixaPorIdAsync(int id, bool transacoesAnteriores);
    Task<TransacaoFixaDto> CriarTransacaoFixaAsync(TransacaoFixaCriacaoDto transacaoFixaDto);
    Task<bool> AtualizarTransacaoFixaAsync(int id, TransacaoFixaAtualizacaoDto transacaoFixaAtualizacaoDto, bool transacaoAnteriores);
    Task CriarTransacoesParaMesAnoAsync(int ano, int mes);
} 