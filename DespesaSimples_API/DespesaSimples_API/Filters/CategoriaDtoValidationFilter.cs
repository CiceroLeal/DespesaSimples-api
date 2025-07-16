using DespesaSimples_API.Dtos.Categoria;

namespace DespesaSimples_API.Filters;

public class CategoriaDtoValidationFilter : BaseDtoValidationFilter<CategoriaFormDto>
{
    protected override Dictionary<string, string[]> ValidateBusinessRules(CategoriaFormDto dto)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(dto.Nome) || dto.Nome.Length < 3 || dto.Nome.Length > 100)
            errors[nameof(dto.Nome)] = ["Nome deve ter entre 3 e 100 caracteres."];

        if (!string.IsNullOrEmpty(dto.Descricao) && dto.Descricao.Length > 500)
            errors[nameof(dto.Descricao)] = ["Descricao pode ter no máximo 500 caracteres."];

        if (dto.Dia is < 1 or > 31)
            errors[nameof(dto.Dia)] = ["Dia deve estar entre 1 e 31."];

        return errors;
    }
}