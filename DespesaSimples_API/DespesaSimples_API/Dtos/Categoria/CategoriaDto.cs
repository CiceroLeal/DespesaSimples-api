using DespesaSimples_API.Dtos.Transacao;

namespace DespesaSimples_API.Dtos.Categoria;

public class CategoriaDto
{
    public string? IdCategoria { get; set; }
    public required string Nome { get; set; }
    public string? Descricao { get; set; }
    public string? IdCategoriaPai { get; set; }
    public string? NomeCategoriaPai { get; set; }
    public int? Dia { get; set; }
    public List<TransacaoDto>? Transacoes { get; set; }
}