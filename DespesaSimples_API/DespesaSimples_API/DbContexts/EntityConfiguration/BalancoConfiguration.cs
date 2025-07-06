using DespesaSimples_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DespesaSimples_API.DbContexts.EntityConfiguration;

public class BalancoConfiguration : IEntityTypeConfiguration<Balanco>
{
    public void Configure(EntityTypeBuilder<Balanco> builder)
    {
        builder.Property(e => e.CreatedAt);
        builder.Property(e => e.UpdatedAt);
        builder.Property(e => e.DeletedAt);
        builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false);
        
        builder.HasOne(b => b.Usuario)
            .WithMany(t => t.Balancos)
            .HasForeignKey(b => b.UsuarioId)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
} 