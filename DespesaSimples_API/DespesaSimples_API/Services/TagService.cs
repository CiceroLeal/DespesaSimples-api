using DespesaSimples_API.Abstractions.Repositories;
using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Dtos;
using DespesaSimples_API.Dtos.Responses;
using DespesaSimples_API.Entities;
using DespesaSimples_API.Exceptions;

namespace DespesaSimples_API.Services;

public class TagService(ITagRepository tagRepository) : ITagService
{
    public async Task<TagResponseDto> ObterTodasTagsAsync()
    {
        var tags = await tagRepository.ObterTodasTagsAsync();
        var tagsDtos = tags.Select(t => new TagDto
        {
            IdTag = t.IdTag,
            Nome = t.Nome
        }).ToList();

        return new TagResponseDto
        {
            Tags = tagsDtos
        };
    }

    public async Task<TagResponseDto> ObterTagPorIdAsync(int id)
    {
        var tag = await tagRepository.ObterTagPorIdAsync(id);

        if (tag == null)
            throw new NotFoundException();

        return new TagResponseDto
        {
            Tags = [new TagDto { IdTag = tag.IdTag, Nome = tag.Nome }]
        };
    }

    public async Task<bool> RemoverTagPorIdAsync(int id)
    {
        return await tagRepository.RemoverTagAsync(id);
    }

    public async Task<bool> CriarTagAsync(TagDto tagDto)
    {
        var tag = new Tag
        {
            Nome = tagDto.Nome
        };

        return await tagRepository.CriarTagAsync(tag);
    }

    public async Task<bool> AtualizarTagAsync(int id, TagDto tagDto)
    {
        var tag = await tagRepository.ObterTagPorIdAsync(id);

        if (tag == null)
            throw new NotFoundException();

        tag.Nome = tagDto.Nome;

        return await tagRepository.AtualizarTagAsync(tag);
    }
}