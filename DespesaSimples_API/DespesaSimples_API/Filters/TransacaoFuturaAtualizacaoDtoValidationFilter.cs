using DespesaSimples_API.Dtos.Transacao;

namespace DespesaSimples_API.Filters;

public class TransacaoFuturaAtualizacaoDtoValidationFilter : BaseDtoValidationFilter<TransacaoFuturaAtualizacaoDto>
{
    protected override Dictionary<string, string[]> ValidateBusinessRules(TransacaoFuturaAtualizacaoDto dto)
    {
        var errors = new Dictionary<string, string[]>();

        if (dto.Valor <= 0)
            errors[nameof(dto.Valor)] = ["O valor da transação deve ser maior que zero."];

        if (string.IsNullOrWhiteSpace(dto.Descricao))
            errors[nameof(dto.Descricao)] = ["A descrição é obrigatória."];

        if (string.IsNullOrWhiteSpace(dto.Categoria) || dto.Categoria == "0")
            dto.Categoria = null;
        
        if(string.IsNullOrWhiteSpace(dto.Cartao) || dto.Cartao == "0")
            dto.Cartao = null;

        return errors;
    }
}