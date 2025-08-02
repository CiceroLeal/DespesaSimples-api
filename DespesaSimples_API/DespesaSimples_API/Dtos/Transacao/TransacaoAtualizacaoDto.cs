namespace DespesaSimples_API.Dtos.Transacao;

public class TransacaoAtualizacaoDto
{
    public required decimal Valor { get; set; }
    public required DateTime DataVencimento { get; set; }
    public DateTime? DataTransacao { get; set; }
    public required string Descricao { get; set; }
    public string? Categoria { get; set; }
    public string? Cartao { get; set; }
    public List<string>? Tags { get; set; }
    public required bool Finalizada { get; set; }
} 