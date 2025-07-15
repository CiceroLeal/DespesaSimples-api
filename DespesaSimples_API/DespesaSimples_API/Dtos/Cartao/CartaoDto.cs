using DespesaSimples_API.Dtos.Categoria;
using DespesaSimples_API.Dtos.Transacao;

namespace DespesaSimples_API.Dtos.Cartao;

public class CartaoDto
{
    public string? IdCartao { get; set; }
    public required decimal Limite { get; set; }
    public required string Nome { get; set; }
    public string? Bandeira { get; set; }
    public string? Descricao { get; set; }
    public int? DiaFechamento { get; set; }
    public required int DiaVencimento { get; set; }
    public string? IdCategoria { get; set; }
    public decimal? ValorParcial { get; set; }
    public List<TransacaoDto>? Transacoes { get; set; }
    public CategoriaDto? Categoria { get; set; }
}