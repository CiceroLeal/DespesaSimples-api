using DespesaSimples_API.Dtos.Tag;
using DespesaSimples_API.Entities;

namespace DespesaSimples_API.Mappers;

public static class TagMapper
{
    public static TagDto MapParaDto(Tag tag)
    {
        return new TagDto
        {
            IdTag = tag.IdTag,
            Nome = tag.Nome
        };
    }

    public static Tag MapParaTag(TagDto tagDto)
    {
        return new Tag
        {
            IdTag = tagDto.IdTag,
            Nome = tagDto.Nome
        };
    }
    
    public static List<Tag> MapParaTag(List<TagDto> tagDto)
    {
        return tagDto.Select(MapParaTag).ToList();
    }
}