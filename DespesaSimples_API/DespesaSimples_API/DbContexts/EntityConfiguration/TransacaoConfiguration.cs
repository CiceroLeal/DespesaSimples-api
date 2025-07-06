using DespesaSimples_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DespesaSimples_API.DbContexts.EntityConfiguration;

public class TransacaoConfiguration : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {
        builder.Property(e => e.CreatedAt);
        builder.Property(e => e.UpdatedAt);
        builder.Property(e => e.DeletedAt);
        builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false);

        builder.HasOne(t => t.Categoria)
            .WithMany(c => c.Transacoes)
            .HasForeignKey(t => t.IdCategoria)
            .OnDelete(DeleteBehavior.ClientCascade);
        
        builder.HasOne(t => t.Cartao)
            .WithMany(c => c.Transacoes)
            .HasForeignKey(t => t.IdCartao)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasOne(t => t.TransacaoFixa)
            .WithMany(tf => tf.Transacoes)
            .HasForeignKey(t => t.IdTransacaoFixa)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Usuario)
            .WithMany(u => u.Transacoes)
            .HasForeignKey(t => t.UsuarioId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasMany(t => t.Tags)
            .WithMany(tag => tag.Transacoes)
            .UsingEntity(e => e.ToTable("TransacaoTags"));
    }
} 