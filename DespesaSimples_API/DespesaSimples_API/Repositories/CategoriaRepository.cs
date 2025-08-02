using DespesaSimples_API.Abstractions.Repositories;
using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.DbContexts;
using DespesaSimples_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DespesaSimples_API.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly DespesaSimplesDbContext _dsContext;

    public CategoriaRepository(DespesaSimplesDbContext context, IUsuarioService usuarioService)
    {
        _dsContext = context;
        _dsContext.CurrentUserId = usuarioService.GetIdUsuarioAtual();
    }

    public async Task<List<Categoria>> BuscarCategoriasAsync()
    {
        return await _dsContext.Categorias
            .Include(g => g.CategoriaPai)
            .Include(g => g.Subcategorias)
            .ToListAsync();
    }

    public async Task<Categoria?> BuscarCategoriaPorIdAsync(int id)
    {
        return await _dsContext.Categorias
            .Include(g => g.CategoriaPai)
            .Include(g => g.Subcategorias)
                .ThenInclude(s => s.Cartoes)
            .Include(g => g.Cartoes)
            .Where(g => g.IdCategoria == id)
            .FirstOrDefaultAsync();
    }
    
    public async Task<List<Categoria>> BuscarCategoriaEPaisAsync(int id)
    {
        var resultado = new List<Categoria>();
        var atual = await _dsContext.Categorias
            .Include(c => c.CategoriaPai)
            .FirstOrDefaultAsync(c => c.IdCategoria == id);
        
        while (atual != null)
        {
            resultado.Add(atual);
            if (atual.IdCategoriaPai == null) 
                break;
            
            atual = await _dsContext.Categorias
                .Include(c => c.CategoriaPai)
                .FirstOrDefaultAsync(c => c.IdCategoria == atual.IdCategoriaPai.Value);
        }

        return resultado;
    }

    public async Task<bool> CriarCategoriaAsync(Categoria categoria)
    {
        categoria.UsuarioId = _dsContext.CurrentUserId ?? "";
        await _dsContext.Categorias.AddAsync(categoria);
        await _dsContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AtualizarCategoriaAsync(Categoria categoria)
    {
        var existCategoria = await _dsContext.Categorias
            .FirstOrDefaultAsync(c => c.IdCategoria == categoria.IdCategoria);
        
        if (existCategoria == null)
            return false;
        
        _dsContext
            .Entry(existCategoria)
            .CurrentValues.SetValues(categoria);
        
        await _dsContext.SaveChangesAsync();
        
        return true;
    }
    
    public async Task<bool> RemoverCategoriaAsync(int id)
    {
        var categoria = await _dsContext.Categorias
            .FirstOrDefaultAsync(c => c.IdCategoria == id);
        
        if (categoria == null)
            return false;
        
        _dsContext.Categorias.Remove(categoria);
        
        var registrosAfetados = await _dsContext.SaveChangesAsync();
        return registrosAfetados > 0;
    }
} 