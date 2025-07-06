using DespesaSimples_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DespesaSimples_API.DbContexts.EntityConfiguration;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.Property(e => e.CreatedAt);
        builder.Property(e => e.UpdatedAt);
        builder.Property(e => e.DeletedAt);
        builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false);

        builder.HasOne(t => t.Usuario)
            .WithMany(t => t.Tags)
            .HasForeignKey(t => t.UsuarioId)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
} 