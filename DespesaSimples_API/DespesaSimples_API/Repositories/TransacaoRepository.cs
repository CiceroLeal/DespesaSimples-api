using System.Linq.Expressions;
using DespesaSimples_API.Abstractions.Repositories;
using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.DbContexts;
using DespesaSimples_API.Entities;
using DespesaSimples_API.Enums;
using Microsoft.EntityFrameworkCore;

namespace DespesaSimples_API.Repositories;

public class TransacaoRepository : ITransacaoRepository
{
    private readonly DespesaSimplesDbContext _dsContext;

    public TransacaoRepository(DespesaSimplesDbContext context, IUsuarioService usuarioService)
    {
        _dsContext = context;
        _dsContext.CurrentUserId = usuarioService.GetIdUsuarioAtual();
    }
    
    public Task<bool> AtualizarDiaTransacoesFuturasPorCategoriaAsync(
        int idCategoria, int novoDia, int anoAtual, int mesAtual) => AtualizarDiaTransacoesFuturasAsync(
            t => t.IdCategoria == idCategoria, 
            novoDia, anoAtual, mesAtual);
    
    private async Task<bool> AtualizarDiaTransacoesFuturasAsync(
        Expression<Func<Transacao, bool>> chaveFilter,
        int novoDia,
        int anoAtual,
        int mesAtual)
    {
        Expression<Func<Transacao,bool>> futuroFilter = t =>
            chaveFilter.Compile().Invoke(t)
            && (t.Ano > anoAtual || (t.Ano == anoAtual && t.Mes >= mesAtual));
        
        await _dsContext.Transacoes
            .Where(futuroFilter)
            .ExecuteUpdateAsync(s => s.SetProperty(t => t.Dia, novoDia));

        return true;
    }
    
    public async Task<decimal> SomarPorTipoAsync(TipoTransacaoEnum tipo, int ano, int mes)
    {
        return await _dsContext.Transacoes
            .Where(t => t.Tipo == tipo && t.Ano == ano && t.Mes == mes)
            .SumAsync(t => t.Valor);
    }
}