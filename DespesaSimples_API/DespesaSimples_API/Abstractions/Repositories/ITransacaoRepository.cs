using DespesaSimples_API.Enums;

namespace DespesaSimples_API.Abstractions.Repositories;

public interface ITransacaoRepository
{
    Task<bool> AtualizarDiaTransacoesFuturasPorCategoriaAsync(int idCategoria, int novoDia, int anoAtual, int mesAtual);
    Task<bool> AtualizarDiaTransacoesFuturasPorCartaoAsync(int idCartao, int novoDia, int anoAtual, int mesAtual);
    
    Task<decimal> SomarPorTipoAsync(TipoTransacaoEnum tipo, int ano, int mes);
}