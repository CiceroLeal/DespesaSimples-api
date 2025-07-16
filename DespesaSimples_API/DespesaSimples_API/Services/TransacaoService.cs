using DespesaSimples_API.Abstractions.Repositories;
using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Enums;

namespace DespesaSimples_API.Services;

public class TransacaoService(ITransacaoRepository transacaoRepository) : ITransacaoService
{
    public async Task<bool> AtualizarDiaTransacoesFuturasAsync(TipoCategoriaEnum tipo, int idCategoria, int novoDia,
        int anoAtual,
        int mesAtual)
    {
        if (tipo == TipoCategoriaEnum.Categoria)
            return await transacaoRepository
                .AtualizarDiaTransacoesFuturasPorCategoriaAsync(idCategoria, novoDia, anoAtual, mesAtual);

        return await transacaoRepository
            .AtualizarDiaTransacoesFuturasPorCartaoAsync(idCategoria, novoDia, anoAtual, mesAtual);
    }

    public async Task<decimal> SomarPorTipoAsync(TipoTransacaoEnum tipo, int ano, int mes)
    {
        return await transacaoRepository
            .SomarPorTipoAsync(
                tipo,
                ano,
                mes);
    }
}