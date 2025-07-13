using DespesaSimples_API.Dtos;
using DespesaSimples_API.Dtos.Tag;

namespace DespesaSimples_API.Abstractions.Services;

public interface ITagService
{
    Task<TagResponseDto> ObterTodasTagsAsync();
    Task<TagResponseDto> ObterTagPorIdAsync(int id);
    Task<bool> RemoverTagPorIdAsync(int id);
    Task<bool> CriarTagAsync(TagDto tagDto);
    Task<bool> AtualizarTagAsync(int id, TagDto tagDto);
}