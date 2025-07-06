using DespesaSimples_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DespesaSimples_API.DbContexts.EntityConfiguration;

public class TransacaoFixaConfiguration : IEntityTypeConfiguration<TransacaoFixa>
{
    public void Configure(EntityTypeBuilder<TransacaoFixa> builder)
    {
        builder.Property(e => e.CreatedAt);
        builder.Property(e => e.UpdatedAt);
        builder.Property(e => e.DeletedAt);
        builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false);

        builder.HasOne(t => t.Categoria)
            .WithMany()
            .HasForeignKey(d => d.IdCategoria)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(t => t.Cartao)
            .WithMany()
            .HasForeignKey(d => d.IdCartao)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Usuario)
            .WithMany(t => t.TransacoesFixa)
            .HasForeignKey(t => t.UsuarioId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasMany(d => d.Tags)
            .WithMany()
            .UsingEntity(j => j.ToTable("TransacaoFixaTags"));
    }
} 