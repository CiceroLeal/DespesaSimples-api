namespace DespesaSimples_API.Dtos.Categoria;

public class CategoriaFormDto
{
    public required string Nome { get; set; }
    public string? Descricao { get; set; }
    public string? IdCategoriaPai { get; set; }
    public int? Dia { get; set; }
} 