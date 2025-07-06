using DespesaSimples_API.Entities;
using DespesaSimples_API.Enums;
using Microsoft.EntityFrameworkCore;

namespace DespesaSimples_API.DbContexts.EntityConfiguration;

public static class SeedDataConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transacao>().HasData(
            new Transacao
            {
                IdTransacao = 1,
                Descricao = "Compra de tomada",
                Valor = (decimal)3.5,
                Dia = 11,
                Ano = 2025,
                Mes = 4,
                IdCartao = 1,
                Status = nameof(StatusTransacaoEnum.AFinalizar),
                Tipo = TipoTransacaoEnum.Despesa,
                UsuarioId = "1"
            },
            new Transacao
            {
                IdTransacao = 2,
                Descricao = "Compra mercado",
                IdCategoria = 1,
                Valor = (decimal)2.25,
                Dia = 12,
                Ano = 2025,
                Mes = 4,
                Status = nameof(StatusTransacaoEnum.Finalizada),
                Tipo = TipoTransacaoEnum.Despesa,
                UsuarioId = "1"
            },
            new Transacao
            {
                IdTransacao = 3,
                Descricao = "Compra mercado",
                IdCategoria = 1,
                Valor = (decimal)30.75,
                Dia = 13,
                Ano = 2025,
                Mes = 4,
                Status = nameof(StatusTransacaoEnum.Finalizada),
                Tipo = TipoTransacaoEnum.Despesa,
                UsuarioId = "1"
            },
            new Transacao
            {
                IdTransacao = 4,
                Descricao = "Compra mercado Sonda",
                IdCategoria = 1,
                Valor = 3,
                Dia = 14,
                Ano = 2025,
                Mes = 4,
                Status = nameof(StatusTransacaoEnum.Vencida),
                Tipo = TipoTransacaoEnum.Despesa,
                UsuarioId = "1"
            },
            new Transacao
            {
                IdTransacao = 5,
                Descricao = "Compra tv",
                IdCategoria = 3,
                Valor = 3,
                Dia = 15,
                Ano = 2025,
                Mes = 4,
                DataTransacao = new DateTime(2025, 5, 10),
                Parcela = "1/3",
                GrupoParcelasId = "1",
                Status = nameof(StatusTransacaoEnum.AFinalizar),
                Tipo = TipoTransacaoEnum.Despesa,
                UsuarioId = "1"
            },
            new Transacao
            {
                IdTransacao = 6,
                Descricao = "Compra tv",
                IdCategoria = 3,
                Valor = 3,
                Dia = 16,
                Ano = 2025,
                Mes = 4,
                DataTransacao = new DateTime(2025, 5, 10),
                Parcela = "2/3",
                GrupoParcelasId = "1",
                Status = nameof(StatusTransacaoEnum.AFinalizar),
                Tipo = TipoTransacaoEnum.Despesa,
                UsuarioId = "1"
            },
            new Transacao
            {
                IdTransacao = 7,
                Descricao = "Compra tv",
                IdCategoria = 3,
                Valor = 3,
                Dia = 17,
                Ano = 2025,
                Mes = 4,
                DataTransacao = new DateTime(2025, 5, 10),
                Parcela = "3/3",
                GrupoParcelasId = "1",
                Status = nameof(StatusTransacaoEnum.AFinalizar),
                Tipo = TipoTransacaoEnum.Despesa,
                UsuarioId = "1"
            },
            new Transacao
            {
                IdTransacao = 8,
                Descricao = "Compra fritadeira",
                IdCategoria = 2,
                Valor = 3,
                Dia = 18,
                Ano = 2025,
                Mes = 4,
                Status = nameof(StatusTransacaoEnum.AFinalizar),
                Tipo = TipoTransacaoEnum.Despesa,
                UsuarioId = "1"
            },
            new Transacao
            {
                IdTransacao = 9,
                Descricao = "Salário",
                Valor = (decimal)5000.62,
                Dia = 10,
                Ano = 2025,
                Mes = 4,
                Status = nameof(StatusTransacaoEnum.AFinalizar),
                Tipo = TipoTransacaoEnum.Receita,
                UsuarioId = "1"
            },
            new Transacao
            {
                IdTransacao = 10,
                Descricao = "Job mercado",
                IdCategoria = 1,
                Valor = (decimal)2.25,
                Dia = 10,
                Ano = 2025,
                Mes = 4,
                Status = nameof(StatusTransacaoEnum.Finalizada),
                Tipo = TipoTransacaoEnum.Receita,
                UsuarioId = "1"
            },
            new Transacao
            {
                IdTransacao = 11,
                Descricao = "Job mercado 2",
                IdCategoria = 1,
                Valor = (decimal)3.75,
                Dia = 10,
                Ano = 2025,
                Mes = 4,
                Status = nameof(StatusTransacaoEnum.Finalizada),
                Tipo = TipoTransacaoEnum.Receita,
                UsuarioId = "1"
            },
            new Transacao
            {
                IdTransacao = 12,
                Descricao = "Job mercado 3",
                IdCategoria = 1,
                Valor = (decimal)3.5,
                Dia = 10,
                Ano = 2025,
                Mes = 4,
                Status = nameof(StatusTransacaoEnum.Vencida),
                Tipo = TipoTransacaoEnum.Receita,
                UsuarioId = "1"
            },
            new Transacao
            {
                IdTransacao = 13,
                Descricao = "Job tv",
                IdCartao = 2,
                Dia = 5,
                Valor = (decimal)3.5,
                Ano = 2025,
                Mes = 4,
                DataTransacao = new DateTime(2025, 5, 10),
                Parcela = "1/3",
                GrupoParcelasId = "2",
                Status = nameof(StatusTransacaoEnum.AFinalizar),
                Tipo = TipoTransacaoEnum.Receita,
                UsuarioId = "1"
            },
            new Transacao
            {
                IdTransacao = 14,
                Descricao = "Job tv",
                IdCartao = 2,
                Dia = 10,
                Valor = (decimal)3.5,
                Ano = 2025,
                Mes = 4,
                DataTransacao = new DateTime(2025, 5, 10),
                Parcela = "2/3",
                GrupoParcelasId = "2",
                Status = nameof(StatusTransacaoEnum.AFinalizar),
                Tipo = TipoTransacaoEnum.Receita,
                UsuarioId = "1"
            },
            new Transacao
            {
                IdTransacao = 15,
                Descricao = "Job tv",
                IdCartao = 2,
                Dia = 15,
                Valor = (decimal)3000.5,
                Ano = 2025,
                Mes = 4,
                DataTransacao = new DateTime(2025, 5, 10),
                Parcela = "3/3",
                GrupoParcelasId = "2",
                Status = nameof(StatusTransacaoEnum.AFinalizar),
                Tipo = TipoTransacaoEnum.Receita,
                UsuarioId = "1"
            },
            new Transacao
            {
                IdTransacao = 16,
                Descricao = "Job fritadeira",
                IdCategoria = 2,
                Dia = 20,
                Valor = (decimal)3.5,
                Ano = 2025,
                Mes = 4,
                Status = nameof(StatusTransacaoEnum.AFinalizar),
                Tipo = TipoTransacaoEnum.Receita,
                UsuarioId = "1"
            },
            new Transacao
            {
                IdTransacao = 17,
                Descricao = "Aluguel",
                Dia = 15,
                Valor = 800,
                Ano = 2025,
                Mes = 4,
                Status = nameof(StatusTransacaoEnum.AFinalizar),
                Tipo = TipoTransacaoEnum.Despesa,
                UsuarioId = "1",
                IdTransacaoFixa = 1
            }
        );

        modelBuilder.Entity<TransacaoFixa>().HasData(
            new TransacaoFixa
            {
                IdTransacaoFixa = 1,
                Descricao = "Aluguel",
                DataInicio = new DateTime(2025, 4, 15),
                Valor = 800,
                Tipo = TipoTransacaoEnum.Despesa,
                UsuarioId = "1"
            },
            new TransacaoFixa
            {
                IdTransacaoFixa = 2,
                Descricao = "Aluguel 2",
                IdCartao = 2,
                DataInicio = new DateTime(2025, 4, 15),
                Valor = 800,
                Tipo = TipoTransacaoEnum.Despesa,
                UsuarioId = "1"
            }
        );

        modelBuilder.Entity<Categoria>().HasData(
            new Categoria
            {
                IdCategoria = 1,
                Nome = "Mercado",
                Dia = 30,
                CreatedAt = new DateTime(2024, 6, 1),
                UsuarioId = "1"
            },
            new Categoria
            {
                IdCategoria = 2,
                Nome = "Eletrodomésticos",
                Dia = 29,
                CreatedAt = new DateTime(2024, 6, 1),
                UsuarioId = "1"
            },
            new Categoria
            {
                IdCategoria = 3,
                IdCategoriaPai = 2,
                Nome = "Tv",
                Dia = 28,
                CreatedAt = new DateTime(2024, 6, 1),
                UsuarioId = "1"
            }
        );

        modelBuilder.Entity<Tag>().HasData(
            new Tag
            {
                IdTag = 1,
                Nome = "alimentação",
                CreatedAt = new DateTime(2024, 6, 1),
                UsuarioId = "1"
            },
            new Tag
            {
                IdTag = 2,
                Nome = "mercado",
                CreatedAt = new DateTime(2024, 6, 1),
                UsuarioId = "1"
            },
            new Tag
            {
                IdTag = 3,
                Nome = "besteira",
                CreatedAt = new DateTime(2024, 6, 1),
                UsuarioId = "1"
            }
        );
        
        modelBuilder.Entity<Balanco>().HasData(
            new Balanco
            {
                IdBalanco = 1,
                Ano = 2025,
                Mes = 3,
                SaldoInicial = 0.00m,
                TotalReceitas = 100.00m,
                TotalDespesas = 0.00m,
                SaldoFinal = 100.00m,
                UsuarioId = "1"
            }
        );
        
        modelBuilder.Entity<Cartao>().HasData(
            new Cartao
            {
                IdCartao = 1,
                Limite = 5000,
                Nome = "Cartão de crédito",
                Bandeira = "Visa",
                DiaFechamento = 5,
                DiaVencimento = 10,
                UsuarioId = "1"
            },
            new Cartao
            {
                IdCartao = 2,
                Limite = 5000,
                Nome = "Cartão de crédito 2",
                Bandeira = "Visa",
                IdCategoria = 3,
                DiaFechamento = 5,
                DiaVencimento = 10,
                UsuarioId = "1"
            }
        );
    }
}