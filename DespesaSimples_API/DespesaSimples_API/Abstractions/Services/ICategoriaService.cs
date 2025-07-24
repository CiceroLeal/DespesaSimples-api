using DespesaSimples_API.Dtos.Categoria;

namespace DespesaSimples_API.Abstractions.Services;

public interface ICategoriaService
{
    Task<CategoriaResponseDto> BuscarCategoriasAsync();
    Task<CategoriaResponseDto> BuscarCategoriaDtoPorIdAsync(string id);
    Task<CategoriaResponseDto> BuscarCategoriaEPaisPorIdAsync(string id);
    Task<bool> RemoverCategoriaPorIdAsync(string id);
    Task<bool> CriarCategoriaAsync(CategoriaFormDto categoriaFormDto);
    Task<bool> AtualizarCategoriaAsync(string id, CategoriaFormDto categoriaAtualizacaoDto);
} 