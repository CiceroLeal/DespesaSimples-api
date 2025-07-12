namespace DespesaSimples_API.Dtos.Responses;

public class TagResponseDto
{
    public IEnumerable<TagDto> Tags { get; set; } = new List<TagDto>();
}