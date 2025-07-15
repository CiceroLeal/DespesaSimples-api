namespace DespesaSimples_API.Dtos.Cartao;

public class CartaoFormDto
{
    public required decimal Limite { get; set; }
    public required string Nome { get; set; }
    public string? Descricao { get; set; }
    public string? Bandeira { get; set; }
    public int? DiaFechamento { get; set; }
    public required int DiaVencimento { get; set; }
    public string? IdCategoria { get; set; }
} 