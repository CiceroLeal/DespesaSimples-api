using DespesaSimples_API.Dtos.TransacaoFixa;

namespace DespesaSimples_API.Filters;

public class TransacaoFixaFormDtoValidationFilter : BaseDtoValidationFilter<TransacaoFixaFormDto>
{
    protected override Dictionary<string, string[]> ValidateBusinessRules(TransacaoFixaFormDto dto)
    {
        var errors = new Dictionary<string, string[]>();
        
        if (dto.Valor <= 0)
            errors[nameof(dto.Valor)] = ["Valor deve ser maior que zero."];
        
        if (string.IsNullOrWhiteSpace(dto.Descricao))
            errors[nameof(dto.Descricao)] = ["Descrição é obrigatória."];
        
        if (dto.DataTermino.HasValue && dto.DataTermino.Value.Date < dto.DataInicio.Date)
            errors[nameof(dto.DataTermino)] = ["Data de término não pode ser anterior à data de início."];
        
        if (string.IsNullOrWhiteSpace(dto.Categoria) || dto.Categoria == "0")
            dto.Categoria = null;
        
        if (string.IsNullOrWhiteSpace(dto.Cartao) || dto.Cartao == "0")
            dto.Cartao = null;
        
        if (dto.Tags is { Count: > 0 } && dto.Tags.Any(string.IsNullOrWhiteSpace))
            errors[nameof(dto.Tags)] = ["Tags não podem conter valores vazios."];
        
        var dataFim = dto.DataTermino ?? DateTime.Now;
        var qtdMeses = (dataFim.Year - dto.DataInicio.Year) * 12 + dataFim.Month - dto.DataInicio.Month;
        if (qtdMeses > 12)
            errors[nameof(dto.DataInicio)] = ["A data de início máxima é de um ano atrás"];

        return errors;
    }
}