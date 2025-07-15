namespace DespesaSimples_API.Dtos.Cartao;

public class CartaoResponseDto
{
    public IEnumerable<CartaoDto> Cartoes { get; set; } = new List<CartaoDto>();
}