using DespesaSimples_API.Dtos.Tag;
using DespesaSimples_API.Entities;

namespace DespesaSimples_API.Abstractions.Services;

public interface ITagService
{
    Task<TagResponseDto> BuscarTodasTagsAsync();
    Task<TagResponseDto> BuscarTagPorIdAsync(int id);
    Task<bool> RemoverTagPorIdAsync(int id);
    Task<bool> CriarTagAsync(TagDto tagDto);
    Task<bool> AtualizarTagAsync(int id, TagDto tagDto);
    Task<List<Tag>> BuscarAtualizarTagsAsync(List<string> nomesTags);
}