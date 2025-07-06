using DespesaSimples_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DespesaSimples_API.DbContexts.EntityConfiguration;

public class CartaoConfiguration : IEntityTypeConfiguration<Cartao>
{
    public void Configure(EntityTypeBuilder<Cartao> builder)
    {
        builder.Property(e => e.CreatedAt);
        builder.Property(e => e.UpdatedAt);
        builder.Property(e => e.DeletedAt);
        builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false);

        builder.HasOne(t => t.Usuario)
            .WithMany(t => t.Cartoes)
            .HasForeignKey(t => t.UsuarioId)
            .OnDelete(DeleteBehavior.ClientCascade);
        
        builder.HasOne(t => t.Categoria)
            .WithMany(c => c.Cartoes)
            .HasForeignKey(t => t.IdCategoria)
            .OnDelete(DeleteBehavior.Restrict);
    }
}