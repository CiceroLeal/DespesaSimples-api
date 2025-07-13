namespace DespesaSimples_API.Dtos.Balanco;

public class BalancoResponseDto
{
    public IEnumerable<BalancoDto> Balancos { get; set; } = new List<BalancoDto>();
}