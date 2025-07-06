using DespesaSimples_API.Dtos;
using DespesaSimples_API.Dtos.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DespesaSimples_API.Filters;

public class ValidateFutureDateFilter(ILogger<ValidateFutureDateFilter> logger) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        // if (context.Arguments
        //         .FirstOrDefault(a => a is TransacaoFuturaAtualizacaoDto) is not TransacaoFuturaAtualizacaoDto dto)
        // {
        //     return await next(context);
        // }
        //
        // var vencimento = dto.DataVencimento;
        // var agora = DateTime.Now;
        //
        // var dataReferencia = new DateTime(vencimento.Year, vencimento.Month, 1);
        // var dataAtual    = new DateTime(agora.Year,    agora.Month,    1);
        //
        // if (dataReferencia <= dataAtual)
        // {
        //     logger.LogWarning(
        //         "Data de vencimento inválida: {Mes}/{Ano} não é posterior a {MesAtual}/{AnoAtual}",
        //         vencimento.Month, vencimento.Year, agora.Month, agora.Year);
        //     
        //     context.Result = new BadRequestObjectResult(new ApiResponse<object>
        //     {
        //         Success = false,
        //         Errors = new List<ApiError> { new ApiError { Code = "INVALID_DATE", Message = "A data informada não pode ser futura." } },
        //         Message = "Erro de validação de data."
        //     });
        //     return null;
        // }
        //
        return await next(context);
    }
}