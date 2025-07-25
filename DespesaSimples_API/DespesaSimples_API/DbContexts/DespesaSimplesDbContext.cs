using System.Linq.Expressions;
using DespesaSimples_API.DbContexts.EntityConfiguration;
using DespesaSimples_API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DespesaSimples_API.DbContexts;

public class DespesaSimplesDbContext(DbContextOptions<DespesaSimplesDbContext> options)
    : IdentityDbContext<User>(options)
{
    public string? CurrentUserId { get; set; }
    public DbSet<Transacao> Transacoes { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Cartao> Cartoes { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<TransacaoFixa> TransacoesFixas { get; set; }
    public DbSet<Balanco> Balancos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TransacaoConfiguration());
        modelBuilder.ApplyConfiguration(new TransacaoFixaConfiguration());
        modelBuilder.ApplyConfiguration(new CartaoConfiguration());
        modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
        modelBuilder.ApplyConfiguration(new TagConfiguration());
        modelBuilder.ApplyConfiguration(new BalancoConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        SeedDataConfiguration.Configure(modelBuilder);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                     .Where(t => typeof(BaseEntity).IsAssignableFrom(t.ClrType)))
        {
            var parameter = Expression.Parameter(entityType.ClrType, "e");
            var method = typeof(EF).GetMethod(nameof(EF.Property))!.MakeGenericMethod(typeof(bool));
            var isDeletedCall = Expression.Call(method, parameter, Expression.Constant(nameof(BaseEntity.IsDeleted)));
            var notDeleted = Expression.Equal(isDeletedCall, Expression.Constant(false));

            var usuarioIdProp = Expression.Property(parameter, "UsuarioId");
            var currentUserProp = Expression.Property(
                Expression.Constant(this),
                nameof(CurrentUserId)
            );
            var sameUser = Expression.Equal(usuarioIdProp, currentUserProp);

            var filter = Expression.AndAlso(notDeleted, sameUser);
            var lambda = Expression.Lambda(filter, parameter);

            modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
        }

        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        SoftDeleteInterceptor.ApplySoftDeleteCascade(this);
        TimestampInterceptor.UpdateTimestamps(this);
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SoftDeleteInterceptor.ApplySoftDeleteCascade(this);
        TimestampInterceptor.UpdateTimestamps(this);
        return base.SaveChangesAsync(cancellationToken);
    }
}