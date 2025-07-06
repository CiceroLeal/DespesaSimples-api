using DespesaSimples_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DespesaSimples_API.DbContexts.EntityConfiguration;

public static class TimestampInterceptor
{
    public static void UpdateTimestamps(DespesaSimplesDbContext context)
    {
        var entries = context.ChangeTracker.Entries()
            .Where(e => e is
                { Entity: BaseEntity, State: EntityState.Added or EntityState.Modified or EntityState.Deleted });

        foreach (var entityEntry in entries)
        {
            var entity = (BaseEntity)entityEntry.Entity;

            switch (entityEntry.State)
            {
                case EntityState.Added:
                    entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entity.UpdatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Deleted:
                    entityEntry.State = EntityState.Modified;
                    entity.IsDeleted = true;
                    entity.DeletedAt = DateTime.UtcNow;
                    break;
            }
        }
    }
}