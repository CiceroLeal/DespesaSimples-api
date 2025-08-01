using DespesaSimples_API.Dtos.Transacao;

namespace DespesaSimples_API.Filters;

public class TransacaoCriacaoDtoValidationFilter : BaseDtoValidationFilter<TransacaoCriacaoDto>
{
    protected override Dictionary<string, string[]> ValidateBusinessRules(TransacaoCriacaoDto dto)
    {
        var errors = new Dictionary<string, string[]>();
 
        if (dto.Valor <= 0)
            errors[nameof(dto.Valor)] = ["O valor da transação deve ser maior que zero."];
 
        if (string.IsNullOrWhiteSpace(dto.Descricao))
            errors[nameof(dto.Descricao)] = ["A descrição é obrigatória."];
 
        if (dto.Parcelas > 500)
            errors[nameof(dto.Parcelas)] = ["O número máximo de parcelas é 500."];
 
        if (dto.Parcelas < 0)
            errors[nameof(dto.Parcelas)] = ["O número de parcelas não pode ser negativo."];
        
        if (string.IsNullOrWhiteSpace(dto.Categoria) || dto.Categoria == "0")
            dto.Categoria = null;
 
        if (string.IsNullOrWhiteSpace(dto.Cartao) || dto.Cartao == "0")
            dto.Cartao = null;
        
        if (dto is { Categoria: not null, Cartao: not null })
            errors["Transacao"] = ["Uma transação não pode ter uma categoria e um cartão ao mesmo tempo."];
 
        if (dto is { Fixa: true, DataTermino: not null } && dto.DataTermino.Value.Date < dto.DataVencimento.Date)
            errors[nameof(dto.DataTermino)] = ["A data de término não pode ser anterior à data de vencimento."];
 
        if (dto.Tags is { Count: > 0 } && dto.Tags.Any(string.IsNullOrWhiteSpace))
            errors[nameof(dto.Tags)] = ["As tags não podem conter valores vazios."];
        
        var dataFim = dto.DataTermino ?? DateTime.Now;
        var qtdMeses = (dataFim.Year - dto.DataVencimento.Year) * 12 + dataFim.Month - dto.DataVencimento.Month;
        if (dto is { Fixa: true, DataTermino: not null } && qtdMeses > 12)
            errors[nameof(dto.DataVencimento)] = ["A data de vencimento máxima é de um ano atrás"];
 
        return errors;
    }
}