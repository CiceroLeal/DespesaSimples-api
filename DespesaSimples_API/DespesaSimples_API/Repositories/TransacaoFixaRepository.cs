using DespesaSimples_API.Abstractions.Repositories;
using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.DbContexts;
using DespesaSimples_API.Entities;
using DespesaSimples_API.Enums;
using Microsoft.EntityFrameworkCore;

namespace DespesaSimples_API.Repositories;

public class TransacaoFixaRepository : ITransacaoFixaRepository
{
    private readonly DespesaSimplesDbContext _dsContext;
    
    public TransacaoFixaRepository(DespesaSimplesDbContext context, IUsuarioService usuarioService)
    {
        _dsContext = context;
        _dsContext.CurrentUserId = usuarioService.GetIdUsuarioAtual();
    }
    
    public async Task<List<TransacaoFixa>> BuscarTodasTransacoesFixasAsync()
    {
        return await _dsContext.TransacoesFixas
            .Include(t => t.Tags)
            .OrderBy(t => t.Descricao)
            .ToListAsync();
    }

    public async Task<TransacaoFixa?> BuscarTransacaoFixaPorIdAsync(int id)
    {
        return await _dsContext.TransacoesFixas
            .Where(t => t.IdTransacaoFixa == id)
            .Include(t => t.Tags)
            .Include(t => t.Categoria)
            .Include(t => t.Cartao)
            .FirstOrDefaultAsync();
    }
    
    public async Task<List<TransacaoFixa>> BuscarTransacoesFixasPorMesAnoIdAsync(int mes, int ano, TipoTransacaoEnum? tipo)
    {
        // Define o primeiro e último dia do mês informado.
        var dataInicioMes = new DateTime(ano, mes, 1);
        var dataFimMes = dataInicioMes.AddMonths(1).AddDays(-1);
        
        IQueryable<TransacaoFixa> query = _dsContext.Set<TransacaoFixa>();
        
        if (tipo.HasValue)
            query = query.Where(t => t.Tipo == tipo.Value);

        return await query
            .Where(t => t.DataInicio <= dataFimMes)
            .Where(t => t.DataTermino == null || t.DataTermino >= dataInicioMes)
            .Include(t => t.Categoria)
            .Include(t => t.Cartao)
            .Include(t => t.Tags)
            .OrderBy(t => t.Descricao)
            .ToListAsync();
    }
    
    public async Task<bool> CriarTransacaoFixaAsync(TransacaoFixa transacaoFixa)
    {
        transacaoFixa.UsuarioId = _dsContext.CurrentUserId ?? "";
        await _dsContext.TransacoesFixas.AddAsync(transacaoFixa);
        await _dsContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AtualizarTransacaoFixaAsync(TransacaoFixa transacaoFixa)
    {
        var existTransacaoFixa = await _dsContext.TransacoesFixas
            .FirstOrDefaultAsync(t => t.IdTransacaoFixa == transacaoFixa.IdTransacaoFixa);
        
        if (existTransacaoFixa == null)
            return false;
        
        _dsContext
            .Entry(existTransacaoFixa)
            .CurrentValues.SetValues(transacaoFixa);
        
        await _dsContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> RemoverTransacaoFixaAsync(int id)
    {
        var transacaoFixa = await _dsContext.TransacoesFixas
            .FirstOrDefaultAsync(t => t.IdTransacaoFixa == id);
        
        if (transacaoFixa == null)
            return false;
        
        _dsContext.TransacoesFixas.Remove(transacaoFixa);
        
        var registrosAfetados = await _dsContext.SaveChangesAsync();
        return registrosAfetados > 0;
    }
} 