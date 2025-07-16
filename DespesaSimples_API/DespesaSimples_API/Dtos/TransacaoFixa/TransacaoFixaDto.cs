using DespesaSimples_API.Dtos.Cartao;
using DespesaSimples_API.Dtos.Categoria;
using DespesaSimples_API.Enums;

namespace DespesaSimples_API.Dtos.TransacaoFixa;

public class TransacaoFixaDto
{
    public int? IdTransacaoFixa { get; set; }
    public decimal Valor { get; set; }
    public required string Descricao { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataTermino { get; set; }
    public int? IdCategoria { get; set; }
    public int? IdCartao { get; set; }
    public bool? Finalizada { get; set; }
    public List<string>? Tags { get; set; }
    public TipoTransacaoEnum Tipo { get; set; }
    public CategoriaDto? Categoria { get; set; }
    public CartaoDto? Cartao { get; set; }
}