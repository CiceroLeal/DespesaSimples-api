using DespesaSimples_API.Abstractions.Repositories;
using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.DbContexts;
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

    public async Task<decimal> SomarPorTipoAsync(TipoTransacaoEnum tipo, int ano, int mes)
    {
        return await _dsContext.Transacoes
            .Where(t => t.Tipo == tipo && t.Ano == ano && t.Mes == mes)
            .SumAsync(t => t.Valor);
    }
}