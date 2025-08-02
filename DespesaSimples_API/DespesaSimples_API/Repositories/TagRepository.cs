using DespesaSimples_API.Abstractions.Repositories;
using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.DbContexts;
using DespesaSimples_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DespesaSimples_API.Repositories;

public class TagRepository : ITagRepository
{
    private readonly DespesaSimplesDbContext _dsContext;

    public TagRepository(DespesaSimplesDbContext context, IUsuarioService usuarioService)
    {
        _dsContext = context;
        _dsContext.CurrentUserId = usuarioService.GetIdUsuarioAtual();
    }

    public async Task<Tag?> BuscarTagPorNomeAsync(string nome)
    {
        return await _dsContext.Tags
            .FirstOrDefaultAsync(t => t.Nome == nome);
    }
    
    public async Task<Tag?> BuscarTagPorIdAsync(int id)
    {
        return await _dsContext.Tags
            .Include(g => g.Transacoes)
            .Where(g => g.IdTag == id)
            .FirstOrDefaultAsync();
    }
    
    public async Task<List<Tag>> BuscarTagsPorIdsAsync(List<int> ids)
    {
        return await _dsContext.Tags
            .Where(t => ids.Contains(t.IdTag))
            .ToListAsync();
    }

    public async Task<List<Tag>> BuscarTodasTagsAsync()
    {
        return await _dsContext.Tags
            .OrderBy(t => t.Nome)
            .ToListAsync();
    }

    public async Task<bool> CriarTagAsync(Tag tag)
    {
        tag.UsuarioId = _dsContext.CurrentUserId ?? "";
        await _dsContext.Tags.AddAsync(tag);
        await _dsContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AtualizarTagAsync(Tag tag)
    {
        var existTag = await _dsContext.Tags
            .FirstOrDefaultAsync(t => t.IdTag == tag.IdTag);
        
        if (existTag == null)
            return false;
        
        _dsContext
            .Entry(existTag)
            .CurrentValues.SetValues(tag);
        
        await _dsContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> RemoverTagAsync(int id)
    {
        var tag = await _dsContext.Tags
            .FirstOrDefaultAsync(t => t.IdTag == id);
        
        if (tag == null)
            return false;
        
        _dsContext.Tags.Remove(tag);
        
        var registrosAfetados = await _dsContext.SaveChangesAsync();
        return registrosAfetados > 0;
    }
    
    public async Task<List<Tag>> UpsertTagsAsync(List<string> nomesTags)
    {
        var tags = new List<Tag>();
        foreach (var nomeTag in nomesTags)
        {
            var tag = await BuscarTagPorNomeAsync(nomeTag);
            if (tag == null)
            {
                tag = new Tag { Nome = nomeTag };
                await CriarTagAsync(tag);
            }

            tags.Add(tag);
        }

        return tags;
    }
} 