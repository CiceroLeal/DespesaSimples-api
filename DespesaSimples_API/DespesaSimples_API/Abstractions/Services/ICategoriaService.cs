using DespesaSimples_API.Dtos.Categoria;

namespace DespesaSimples_API.Abstractions.Services;

public interface ICategoriaService
{
    Task<List<CategoriaDto>> BuscarCategoriasAsync();
    Task<CategoriaDto?> BuscarCategoriaDtoPorIdAsync(string id);
    Task<List<CategoriaDto>> BuscarCategoriaEPaisPorIdAsync(string id);
    Task<bool> RemoverCategoriaPorIdAsync(string id);
    Task<bool> CriarCategoriaAsync(CategoriaFormDto categoriaFormDto);
    Task<bool> AtualizarCategoriaAsync(string id, CategoriaFormDto categoriaAtualizacaoDto);
}