using DespesaSimples_API.Dtos.Cartao;

namespace DespesaSimples_API.Filters;

public class CartaoDtoValidationFilter : BaseDtoValidationFilter<CartaoFormDto>
{
    protected override Dictionary<string, string[]> ValidateBusinessRules(CartaoFormDto dto)
    {
        var errors = new Dictionary<string, string[]>();

        if (dto.Limite <= 0)
            errors[nameof(dto.Limite)] = ["Limite deve ser maior que zero."];

        if (string.IsNullOrWhiteSpace(dto.Nome) || dto.Nome.Length < 3 || dto.Nome.Length > 100)
            errors[nameof(dto.Nome)] = ["Nome deve ter entre 3 e 100 caracteres."];

        if (!string.IsNullOrEmpty(dto.Bandeira) && dto.Bandeira.Length > 50)
            errors[nameof(dto.Bandeira)] = ["Bandeira pode ter no máximo 50 caracteres."];

        if (!string.IsNullOrEmpty(dto.Descricao) && dto.Descricao.Length > 500)
            errors[nameof(dto.Descricao)] = ["Descricao pode ter no máximo 500 caracteres."];

        if (dto.DiaFechamento is < 1 or > 31)
            errors[nameof(dto.DiaFechamento)] = ["DiaFechamento deve estar entre 1 e 31."];

        if (dto.DiaVencimento is < 1 or > 31)
            errors[nameof(dto.DiaVencimento)] = ["DiaVencimento deve estar entre 1 e 31."];

        return errors;
    }
}