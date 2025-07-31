using DespesaSimples_API.Abstractions.Repositories;
using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Dtos.Tag;
using DespesaSimples_API.Entities;
using DespesaSimples_API.Exceptions;
using DespesaSimples_API.Mappers;

namespace DespesaSimples_API.Services;

public class TagService(ITagRepository tagRepository) : ITagService
{
    public async Task<List<TagDto>> BuscarTodasTagsAsync()
    {
        var tags = await tagRepository.BuscarTodasTagsAsync();
        return tags.Select(TagMapper.MapParaDto).ToList();
    }

    public async Task<TagDto?> BuscarTagPorIdAsync(int id)
    {
        var tag = await tagRepository.BuscarTagPorIdAsync(id);

        return tag != null ? TagMapper.MapParaDto(tag) : null;
    }

    public async Task<bool> RemoverTagPorIdAsync(int id)
    {
        return await tagRepository.RemoverTagAsync(id);
    }

    public async Task<bool> CriarTagAsync(TagDto tagDto)
    {
        var tag = TagMapper.MapParaTag(tagDto);

        return await tagRepository.CriarTagAsync(tag);
    }

    public async Task<bool> AtualizarTagAsync(int id, TagDto tagDto)
    {
        var tag = await tagRepository.BuscarTagPorIdAsync(id);

        if (tag == null)
            throw new NotFoundException();

        tag.Nome = tagDto.Nome;

        return await tagRepository.AtualizarTagAsync(tag);
    }

    public async Task<List<Tag>> BuscarAtualizarTagsAsync(List<string> nomesTags)
    {
        var tags = new List<Tag>();
        foreach (var nomeTag in nomesTags)
        {
            var tag = await tagRepository.BuscarTagPorNomeAsync(nomeTag);
            if (tag == null)
            {
                tag = new Tag { Nome = nomeTag };
                await tagRepository.CriarTagAsync(tag);
            }

            tags.Add(tag);
        }

        return tags;
    }
}