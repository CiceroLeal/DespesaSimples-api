using DespesaSimples_API.Entities;

namespace DespesaSimples_API.Abstractions.Repositories;

public interface ICategoriaRepository
{
    Task<List<Categoria>> BuscarCategoriasAsync();
    Task<Categoria?> ObterCategoriaPorIdAsync(int id);
    Task<bool> RemoverCategoriaAsync(int id);
    Task<bool> CriarCategoriaAsync(Categoria categoria);
    Task<bool> AtualizarCategoriaAsync(Categoria categoria);
    Task<List<Categoria>> ObterCategoriaEPaisAsync(int id);
} 