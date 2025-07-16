using DespesaSimples_API.Enums;

namespace DespesaSimples_API.Abstractions.Services;

public interface ITransacaoService
{
    Task<bool> AtualizarDiaTransacoesFuturasAsync(TipoCategoriaEnum tipo, int idCategoria, int novoDia, int anoAtual, int mesAtual);
    Task<decimal> SomarPorTipoAsync(TipoTransacaoEnum tipo, int ano, int mes);
}