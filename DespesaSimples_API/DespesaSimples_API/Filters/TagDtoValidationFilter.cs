using DespesaSimples_API.Dtos.Tag;

namespace DespesaSimples_API.Filters;

public class TagDtoValidationFilter : BaseDtoValidationFilter<TagDto>
{
    protected override Dictionary<string, string[]> ValidateBusinessRules(TagDto dto)
    {
        var errors = new Dictionary<string, string[]>();

        if (dto.IdTag < 1)
            errors[nameof(dto.IdTag)] = ["IdTag deve ser maior que zero."];

        if (string.IsNullOrWhiteSpace(dto.Nome) || dto.Nome.Length > 100)
            errors[nameof(dto.Nome)] = ["Nome deve ter entre 1 e 100 caracteres."];

        return errors;
    }
}