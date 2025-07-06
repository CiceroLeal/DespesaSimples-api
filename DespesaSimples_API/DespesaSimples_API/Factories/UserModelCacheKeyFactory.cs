using DespesaSimples_API.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DespesaSimples_API.Factories;

public class UserModelCacheKeyFactory : IModelCacheKeyFactory
{
    public object Create(DbContext context, bool designTime)
    {
        return (context.GetType(),
            ((DespesaSimplesDbContext)context).CurrentUserId,
            designTime);
    }
}