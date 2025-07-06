using DespesaSimples_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DespesaSimples_API.DbContexts.EntityConfiguration;

public class CategoriaConfiguration :IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.Property(e => e.CreatedAt);
        builder.Property(e => e.UpdatedAt);
        builder.Property(e => e.DeletedAt);
        builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false);

        builder.HasOne(g => g.CategoriaPai)
            .WithMany(g => g.Subcategorias)
            .HasForeignKey(g => g.IdCategoriaPai)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Usuario)
            .WithMany(t => t.Categorias)
            .HasForeignKey(t => t.UsuarioId)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
} 