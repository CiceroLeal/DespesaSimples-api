using DespesaSimples_API.Enums;

namespace DespesaSimples_API.Abstractions.Repositories;

public interface ITransacaoRepository
{
    Task<decimal> SomarPorTipoAsync(TipoTransacaoEnum tipo, int ano, int mes);
}