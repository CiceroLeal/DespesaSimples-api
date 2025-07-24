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

    public async Task<Transacao?> BuscarTransacaoPorIdAsync(int id)
    {
        return await _dsContext.Transacoes
            .Where(d => d.IdTransacao == id)
            .Include(d => d.Tags)
            .FirstOrDefaultAsync();
    }

    public async Task<Transacao?> BuscarTransacaoPorIdFixaMesAnoAsync(int idTransacaoFixa, int mes, int ano)
    {
        return await _dsContext.Transacoes
            .Where(t => t.IdTransacaoFixa == idTransacaoFixa)
            .Where(t => t.Mes == mes)
            .Where(t => t.Ano == ano)
            .FirstOrDefaultAsync();
    }
    
    public async Task<Transacao?> BuscarUltimaTransacaoPorIdFixaAsync(int id)
    {
        return await _dsContext.Transacoes
            .Where(d => d.IdTransacaoFixa == id)
            .OrderByDescending(t => t.Ano)
            .ThenByDescending(t => t.Mes)
            .FirstOrDefaultAsync();
    }
    
    public async Task<List<Transacao>> BuscarTransacaoPorIdFixaAsync(int idTransacaoFixa)
    {
        return await _dsContext.Transacoes
            .Where(t => t.IdTransacaoFixa == idTransacaoFixa)
            .Include(t => t.Tags)
            .ToListAsync();
    }
    
    public async Task<decimal> SomarPorTipoAsync(TipoTransacaoEnum tipo, int ano, int mes)
    {
        return await _dsContext.Transacoes
            .Where(t => t.Tipo == tipo && t.Ano == ano && t.Mes == mes)
            .SumAsync(t => t.Valor);
    }

    public async Task<bool> CriarTransacaoAsync(Transacao transacao)
    {
        transacao.UsuarioId = _dsContext.CurrentUserId ?? "";
        await _dsContext.Transacoes.AddAsync(transacao);
        await _dsContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AtualizarTransacaoAsync(Transacao transacao)
    {
        await _dsContext.SaveChangesAsync();
        return true;
    }

    public Task<bool> AtualizarDiaTransacoesFuturasPorCategoriaAsync(
        int idCategoria, int novoDia, int anoAtual, int mesAtual) => AtualizarDiaTransacoesFuturasAsync(
        t => t.IdCategoria == idCategoria,
        novoDia, anoAtual, mesAtual);

    public Task<bool> AtualizarDiaTransacoesFuturasPorCartaoAsync(
        int idCartao, int novoDia, int anoAtual, int mesAtual)
        => AtualizarDiaTransacoesFuturasAsync(
            t => t.IdCartao == idCartao,
            novoDia, anoAtual, mesAtual);

    private async Task<bool> AtualizarDiaTransacoesFuturasAsync(
        Expression<Func<Transacao, bool>> chaveFilter,
        int novoDia,
        int anoAtual,
        int mesAtual)
    {
        Expression<Func<Transacao, bool>> futuroFilter = t =>
            chaveFilter.Compile().Invoke(t)
            && (t.Ano > anoAtual || (t.Ano == anoAtual && t.Mes >= mesAtual));

        await _dsContext.Transacoes
            .Where(futuroFilter)
            .ExecuteUpdateAsync(s => s.SetProperty(t => t.Dia, novoDia));

        return true;
    }

    public async Task<bool> RemoverTransacaoAsync(int id)
    {
        var transacao = await _dsContext.Transacoes
            .FirstOrDefaultAsync(t => t.IdTransacao == id);

        if (transacao == null)
            return false;

        _dsContext.Transacoes.Remove(transacao);

        var registrosAfetados = await _dsContext.SaveChangesAsync();

        return registrosAfetados > 0;
    }
    
    public async Task<bool> RemoverTransacoesPorIdTransacaoFixaAsync(int idTransacaoFixa)
    {
        var transacoes = _dsContext.Transacoes
            .Where(t => t.IdTransacaoFixa == idTransacaoFixa);

        _dsContext.Transacoes.RemoveRange(transacoes);

        var registrosAfetados = await _dsContext.SaveChangesAsync();

        return registrosAfetados > 0;
    }
}