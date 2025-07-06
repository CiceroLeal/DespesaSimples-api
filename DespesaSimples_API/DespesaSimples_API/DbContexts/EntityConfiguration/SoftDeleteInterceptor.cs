using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DespesaSimples_API.DbContexts.EntityConfiguration;

public static class SoftDeleteInterceptor
{
    public static void ApplySoftDeleteCascade(DespesaSimplesDbContext context)
    {
        var toCascade = context.ChangeTracker
            .Entries()
            .Where(e => e.State == EntityState.Deleted)
            .ToList();

        foreach (var child in from entry in toCascade
                 let entityType = entry.Metadata
                 let pkProp = entityType.FindPrimaryKey()!.Properties[0]
                 let pkValue = entry.Property(pkProp.Name).CurrentValue
                 let fks = entityType.GetReferencingForeignKeys()
                     .Where(fk =>
                         fk.DeleteBehavior is DeleteBehavior.Cascade or DeleteBehavior.ClientCascade
                         && !fk.DeclaringEntityType.HasSharedClrType
                     )
                 from fk in fks
                 let childType = fk.DeclaringEntityType.ClrType
                 let fkName = fk.Properties[0].Name
                 let childSet = GetDbSetByType(context, childType)
                 let param = Expression.Parameter(childType, "e")
                 let left = Expression.Property(param, fkName)
                 let right = Expression.Constant(pkValue, left.Type)
                 let equal = Expression.Equal(left, right)
                 let predicate = Expression.Lambda(equal, param)
                 let whereCall = Expression.Call(
                     typeof(Queryable), nameof(Queryable.Where), [childType],
                     childSet.Expression, predicate
                 )
                 select childSet
                     .Provider
                     .CreateQuery(whereCall)
                     .Cast<object>()
                     .ToList()
                 into children
                 from child in children
                 select child)
        {
            context.Entry(child).State = EntityState.Deleted;
        }
    }

    private static IQueryable GetDbSetByType(DespesaSimplesDbContext context, Type entityType)
    {
        var mi = typeof(DbContext)
            .GetMethod(nameof(DbContext.Set), Type.EmptyTypes)!
            .MakeGenericMethod(entityType);

        return (IQueryable)mi.Invoke(context, null)!;
    }
}