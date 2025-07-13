using DespesaSimples_API.Dtos.Categoria;

namespace DespesaSimples_API.Abstractions.Services;

public interface ICategoriaService
{
    Task<CategoriaResponseDto> ObterCategoriasAsync();
    Task<CategoriaResponseDto> ObterCategoriaDtoPorIdAsync(string id);
    Task<CategoriaResponseDto> ObterCategoriaEPaisPorIdAsync(string id);
    Task<bool> RemoverCategoriaPorIdAsync(string id);
    Task<bool> CriarCategoriaAsync(CategoriaFormDto categoriaFormDto);
    Task<bool> AtualizarCategoriaAsync(string id, CategoriaFormDto categoriaAtualizacaoDto);
} 