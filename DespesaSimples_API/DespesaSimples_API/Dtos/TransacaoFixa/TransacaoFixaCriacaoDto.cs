using DespesaSimples_API.Enums;

namespace DespesaSimples_API.Dtos.TransacaoFixa;

public class TransacaoFixaCriacaoDto
{
    public decimal Valor { get; set; }
    public required string Descricao { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataTermino { get; set; }
    public string? Categoria { get; set; }
    public string? Cartao { get; set; }
    public bool? Finalizada { get; set; }
    public List<string>? Tags { get; set; }
    public TipoTransacaoEnum Tipo { get; set; }
} 