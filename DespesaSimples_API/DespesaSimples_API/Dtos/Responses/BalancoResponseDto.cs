namespace DespesaSimples_API.Dtos.Responses;

public class BalancoResponseDto
{
    public IEnumerable<BalancoDto> Balancos { get; set; } = new List<BalancoDto>();
}