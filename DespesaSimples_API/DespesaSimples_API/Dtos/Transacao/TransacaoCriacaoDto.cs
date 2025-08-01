using DespesaSimples_API.Enums;

namespace DespesaSimples_API.Dtos.Transacao;

public class TransacaoCriacaoDto
{
    public required decimal Valor { get; set; }
    public required DateTime DataVencimento { get; set; }
    public DateTime? DataTermino { get; set; }
    public DateTime? DataTransacao { get; set; }
    public required string Descricao { get; set; }
    public string? Categoria { get; set; }
    public string? Cartao { get; set; }
    public required TipoTransacaoEnum Tipo { get; set; }
    public List<string>? Tags { get; set; }
    public required bool Fixa { get; set; }
    public required bool Finalizada { get; set; }
    public int Parcelas { get; set; }
}