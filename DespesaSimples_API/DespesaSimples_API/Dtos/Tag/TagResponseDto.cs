namespace DespesaSimples_API.Dtos.Tag;

public class TagResponseDto
{
    public IEnumerable<TagDto> Tags { get; set; } = new List<TagDto>();
}