using DespesaSimples_API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DespesaSimples_API.DbContexts.EntityConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        var hasher = new PasswordHasher<User>();

        var user = new User
        {
            Id = "1",
            Nome = "Admin",
            UserName = "Admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@despesasimples.com.br",
            NormalizedEmail = "ADMIN@DESPESASIMPLES.COM.BR",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("D")
        };

        user.PasswordHash = hasher.HashPassword(user, "123456");

        builder.HasData(user);
    }
} 