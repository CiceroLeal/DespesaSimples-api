using DespesaSimples_API.Entities;
using DespesaSimples_API.Enums;

namespace DespesaSimples_API.Abstractions.Repositories;

public interface ITransacaoFixaRepository
{
    Task<TransacaoFixa?> ObterTransacaoFixaPorIdAsync(int id);
    Task<List<TransacaoFixa>> ObterTransacoesFixasPorMesAnoIdAsync(int mes, int ano, TipoTransacaoEnum? tipo);
    Task<bool> CriarTransacaoFixaAsync(TransacaoFixa transacaoFixa);
    Task<List<TransacaoFixa>> ObterTodasTransacoesFixasAsync();
    Task<bool> RemoverTransacaoFixaAsync(int id);
    Task<bool> AtualizarTransacaoFixaAsync(TransacaoFixa transacaoFixa);
} 