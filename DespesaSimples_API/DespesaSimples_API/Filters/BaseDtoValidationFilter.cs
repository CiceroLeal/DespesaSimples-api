using Microsoft.AspNetCore.Http;

namespace DespesaSimples_API.Filters;

public abstract class BaseDtoValidationFilter<T> : IEndpointFilter where T : class
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var dto = context.Arguments.OfType<T>().FirstOrDefault();
        
        if (dto is null) 
            return await next(context);
        
        var errors = ValidateBusinessRules(dto);
        
        if (errors.Count != 0)
            return Util.ApiResultsUtil.BadRequest(errors);

        return await next(context);
    }

    protected abstract Dictionary<string, string[]> ValidateBusinessRules(T dto);
}