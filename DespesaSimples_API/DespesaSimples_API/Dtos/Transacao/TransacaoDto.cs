using DespesaSimples_API.Dtos.Cartao;
using DespesaSimples_API.Dtos.Categoria;
using DespesaSimples_API.Enums;

namespace DespesaSimples_API.Dtos.Transacao;

public class TransacaoDto
{
    public required string IdTransacao { get; set; }
    public string? Descricao { get; set; }
    public decimal Valor { get; set; }
    public int? Dia { get; set; }
    public int Ano { get; set; }
    public int Mes { get; set; }
    public DateTime? DataTransacao { get; set; }
    public string? Parcela { get; set; }
    public string? Status { get; set; }
    public int? IdCategoria { get; set; }
    public int? IdCartao { get; set; }
    public List<string>? Tags { get; set; }
    public TipoTransacaoEnum? Tipo { get; set; }
    public required List<TransacaoDto> SubTransacoes { get; set; }
    public CategoriaDto? Categoria { get; set; }
    public CartaoDto? Cartao { get; set; }

    public TransacaoDto Clone()
    {
        return new TransacaoDto
        {
            IdTransacao = IdTransacao,
            Descricao = Descricao,
            Valor = Valor,
            Dia = Dia,
            Ano = Ano,
            Mes = Mes,
            DataTransacao = DataTransacao,
            Parcela = Parcela,
            Status = Status,
            IdCategoria = IdCategoria,
            IdCartao = IdCartao,
            Tags = Tags is not null
                ? [..Tags]
                : null,
            Tipo = Tipo,
            SubTransacoes = []
        };
    }
}