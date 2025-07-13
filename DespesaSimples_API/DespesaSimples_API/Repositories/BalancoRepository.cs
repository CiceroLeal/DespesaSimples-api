using DespesaSimples_API.Abstractions.Repositories;
using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.DbContexts;
using DespesaSimples_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DespesaSimples_API.Repositories
{
    public class BalancoRepository : IBalancoRepository
    {
        private readonly DespesaSimplesDbContext _dsContext;

        public BalancoRepository(DespesaSimplesDbContext context, IUsuarioService usuarioService)
        {
            _dsContext = context;
            _dsContext.CurrentUserId = usuarioService.GetIdUsuarioAtual();
        }
        
        public async Task<Balanco?> ObterPorIdAsync(int id)
        {
            return await _dsContext.Balancos
                .FirstOrDefaultAsync(b => b.IdBalanco == id);
        }

        public async Task<Balanco?> ObterPorAnoMesAsync(int ano, int mes)
        {
            return await _dsContext.Balancos
                .FirstOrDefaultAsync(b => b.Ano == ano && b.Mes == mes);
        }
        
        public async Task<List<Balanco>> ObterPorAnoAsync(int ano)
        {
            return await _dsContext.Balancos
                .Where(b => b.Ano == ano)
                .ToListAsync();
        }

        public async Task<List<Balanco>> ObterTodosAsync()
        {
            return await _dsContext.Balancos.ToListAsync();
        }

        public async Task<bool> CriarBalancoAsync(Balanco balanco)
        {
            balanco.UsuarioId = _dsContext.CurrentUserId ?? "";
            await _dsContext.Balancos.AddAsync(balanco);
            await _dsContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AtualizarBalancosync(Balanco balanco)
        {
            _dsContext.Balancos.Update(balanco);
            await _dsContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoverBalancoAsync(Balanco balanco)
        {
            _dsContext.Balancos.Remove(balanco);
            await _dsContext.SaveChangesAsync();
            return true;
        }
    }
} 