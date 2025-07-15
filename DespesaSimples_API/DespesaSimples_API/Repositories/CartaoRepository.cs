using DespesaSimples_API.Abstractions.Repositories;
using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.DbContexts;
using DespesaSimples_API.Entities;
using DespesaSimples_API.Enums;
using Microsoft.EntityFrameworkCore;

namespace DespesaSimples_API.Repositories;

public class CartaoRepository : ICartaoRepository
{
    private readonly DespesaSimplesDbContext _dsContext;

    public CartaoRepository(DespesaSimplesDbContext context, IUsuarioService usuarioService)
    {
        _dsContext = context;
        _dsContext.CurrentUserId = usuarioService.GetIdUsuarioAtual();
    }

    public async Task<List<Cartao>> BuscarCartoesAsync()
    {
        return await _dsContext.Cartoes
            .Include(g => g.Categoria)
            .ToListAsync();
    }
    
    public async Task<List<Cartao>> BuscarCartoesComGastosDoMesAsync(int mes, int ano)
    {
        var qq = await _dsContext.Cartoes
            .Select(c => new 
            {
                Cartao      = c,
                TotalDb     = c.Transacoes
                    .Where(t => t.Ano == ano && t.Mes == mes)
                    .Sum(t =>
                            t.Tipo == TipoTransacaoEnum.Despesa
                                ? t.Valor
                                : -t.Valor
                    )
            })
            .ToListAsync();
        
        foreach (var item in qq)
        {
            item.Cartao.TotalMes = item.TotalDb;
        }
        
        return qq.Select(x => x.Cartao).ToList();
    }

    public async Task<Cartao?> ObterCartaoPorIdAsync(int id)
    {
        return await _dsContext.Cartoes
            .Include(g => g.Categoria)
            .Where(g => g.IdCartao == id)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> RemoverCartaoAsync(int id)
    {
        var cartao = await _dsContext.Cartoes
            .FirstOrDefaultAsync(c => c.IdCartao == id);

        if (cartao == null)
            return false;

        _dsContext.Cartoes.Remove(cartao);

        var registrosAfetados = await _dsContext.SaveChangesAsync();
        return registrosAfetados > 0;
    }

    public async Task<bool> CriarCartaoAsync(Cartao cartao)
    {
        cartao.UsuarioId = _dsContext.CurrentUserId ?? "";
        await _dsContext.Cartoes.AddAsync(cartao);
        await _dsContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AtualizarCartaoAsync(Cartao cartao)
    {
        var existCartao = await _dsContext.Cartoes
            .FirstOrDefaultAsync(c => c.IdCartao == cartao.IdCartao);

        if (existCartao == null)
            return false;

        _dsContext
            .Entry(existCartao)
            .CurrentValues.SetValues(cartao);

        await _dsContext.SaveChangesAsync();
        return true;
    }
}