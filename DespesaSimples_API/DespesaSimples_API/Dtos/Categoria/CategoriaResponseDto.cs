namespace DespesaSimples_API.Dtos.Categoria;

public class CategoriaResponseDto
{
    public IEnumerable<CategoriaDto> Categorias { get; set; } = new List<CategoriaDto>();
}