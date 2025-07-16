namespace DespesaSimples_API.Dtos.TransacaoFixa;

public class TransacaoFixaResponseDto
{
    public IEnumerable<TransacaoFixaDto> Transacoes { get; set; } = new List<TransacaoFixaDto>();
}