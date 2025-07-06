using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DespesaSimples_API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nome = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Balancos",
                columns: table => new
                {
                    IdBalanco = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    Mes = table.Column<int>(type: "int", nullable: false),
                    SaldoInicial = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TotalReceitas = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TotalDespesas = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SaldoFinal = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    UsuarioId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balancos", x => x.IdBalanco);
                    table.ForeignKey(
                        name: "FK_Balancos_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdCategoriaPai = table.Column<int>(type: "int", nullable: true),
                    Nome = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Dia = table.Column<int>(type: "int", nullable: true),
                    UsuarioId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.IdCategoria);
                    table.ForeignKey(
                        name: "FK_Categorias_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Categorias_Categorias_IdCategoriaPai",
                        column: x => x.IdCategoriaPai,
                        principalTable: "Categorias",
                        principalColumn: "IdCategoria",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    IdTag = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.IdTag);
                    table.ForeignKey(
                        name: "FK_Tags_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cartoes",
                columns: table => new
                {
                    IdCartao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Limite = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Nome = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Bandeira = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DiaFechamento = table.Column<int>(type: "int", nullable: true),
                    DiaVencimento = table.Column<int>(type: "int", nullable: false),
                    IdCategoria = table.Column<int>(type: "int", nullable: true),
                    UsuarioId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartoes", x => x.IdCartao);
                    table.ForeignKey(
                        name: "FK_Cartoes_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cartoes_Categorias_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categorias",
                        principalColumn: "IdCategoria",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TransacoesFixas",
                columns: table => new
                {
                    IdTransacaoFixa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataTermino = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IdCategoria = table.Column<int>(type: "int", nullable: true),
                    IdCartao = table.Column<int>(type: "int", nullable: true),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransacoesFixas", x => x.IdTransacaoFixa);
                    table.ForeignKey(
                        name: "FK_TransacoesFixas_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransacoesFixas_Cartoes_IdCartao",
                        column: x => x.IdCartao,
                        principalTable: "Cartoes",
                        principalColumn: "IdCartao",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransacoesFixas_Categorias_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categorias",
                        principalColumn: "IdCategoria",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TransacaoFixaTags",
                columns: table => new
                {
                    TagsIdTag = table.Column<int>(type: "int", nullable: false),
                    TransacaoFixaIdTransacaoFixa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransacaoFixaTags", x => new { x.TagsIdTag, x.TransacaoFixaIdTransacaoFixa });
                    table.ForeignKey(
                        name: "FK_TransacaoFixaTags_Tags_TagsIdTag",
                        column: x => x.TagsIdTag,
                        principalTable: "Tags",
                        principalColumn: "IdTag",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransacaoFixaTags_TransacoesFixas_TransacaoFixaIdTransacaoFi~",
                        column: x => x.TransacaoFixaIdTransacaoFixa,
                        principalTable: "TransacoesFixas",
                        principalColumn: "IdTransacaoFixa",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transacoes",
                columns: table => new
                {
                    IdTransacao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Dia = table.Column<int>(type: "int", nullable: true),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    Mes = table.Column<int>(type: "int", nullable: false),
                    DataTransacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Parcela = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdCategoria = table.Column<int>(type: "int", nullable: true),
                    IdCartao = table.Column<int>(type: "int", nullable: true),
                    IdTransacaoFixa = table.Column<int>(type: "int", nullable: true),
                    GrupoParcelasId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacoes", x => x.IdTransacao);
                    table.ForeignKey(
                        name: "FK_Transacoes_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transacoes_Cartoes_IdCartao",
                        column: x => x.IdCartao,
                        principalTable: "Cartoes",
                        principalColumn: "IdCartao");
                    table.ForeignKey(
                        name: "FK_Transacoes_Categorias_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categorias",
                        principalColumn: "IdCategoria");
                    table.ForeignKey(
                        name: "FK_Transacoes_TransacoesFixas_IdTransacaoFixa",
                        column: x => x.IdTransacaoFixa,
                        principalTable: "TransacoesFixas",
                        principalColumn: "IdTransacaoFixa",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TransacaoTags",
                columns: table => new
                {
                    TagsIdTag = table.Column<int>(type: "int", nullable: false),
                    TransacoesIdTransacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransacaoTags", x => new { x.TagsIdTag, x.TransacoesIdTransacao });
                    table.ForeignKey(
                        name: "FK_TransacaoTags_Tags_TagsIdTag",
                        column: x => x.TagsIdTag,
                        principalTable: "Tags",
                        principalColumn: "IdTag",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransacaoTags_Transacoes_TransacoesIdTransacao",
                        column: x => x.TransacoesIdTransacao,
                        principalTable: "Transacoes",
                        principalColumn: "IdTransacao",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Nome", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "c0b66f0c-738e-4edb-893b-233b486a989e", "admin@despesasimples.com.br", true, false, null, "Admin", "ADMIN@DESPESASIMPLES.COM.BR", "ADMIN", "AQAAAAIAAYagAAAAEGCo4XC0oyrh5JMjgkTcNG3tc4JzQuMWPIJOFBKlBHI803DErwTelkRkfMIewwCpLQ==", null, false, "73ded48e-73d3-4a04-ae57-42746460a5e0", false, "Admin" });

            migrationBuilder.InsertData(
                table: "Balancos",
                columns: new[] { "IdBalanco", "Ano", "created_at", "deleted_at", "Mes", "SaldoFinal", "SaldoInicial", "TotalDespesas", "TotalReceitas", "updated_at", "UsuarioId" },
                values: new object[] { 1, 2025, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, 100.00m, 0.00m, 0.00m, 100.00m, null, "1" });

            migrationBuilder.InsertData(
                table: "Cartoes",
                columns: new[] { "IdCartao", "Bandeira", "created_at", "deleted_at", "Descricao", "DiaFechamento", "DiaVencimento", "IdCategoria", "Limite", "Nome", "updated_at", "UsuarioId" },
                values: new object[] { 1, "Visa", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 5, 10, null, 5000m, "Cartão de crédito", null, "1" });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "IdCategoria", "created_at", "deleted_at", "Descricao", "Dia", "IdCategoriaPai", "Nome", "updated_at", "UsuarioId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 6, 14, 8, 55, 988, DateTimeKind.Utc).AddTicks(8634), null, null, 30, null, "Mercado", null, "1" },
                    { 2, new DateTime(2025, 7, 6, 14, 8, 55, 988, DateTimeKind.Utc).AddTicks(8889), null, null, 29, null, "Eletrodomésticos", null, "1" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "IdTag", "created_at", "deleted_at", "Nome", "updated_at", "UsuarioId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 6, 14, 8, 55, 988, DateTimeKind.Utc).AddTicks(9583), null, "alimentação", null, "1" },
                    { 2, new DateTime(2025, 7, 6, 14, 8, 55, 988, DateTimeKind.Utc).AddTicks(9693), null, "mercado", null, "1" },
                    { 3, new DateTime(2025, 7, 6, 14, 8, 55, 988, DateTimeKind.Utc).AddTicks(9694), null, "besteira", null, "1" }
                });

            migrationBuilder.InsertData(
                table: "Transacoes",
                columns: new[] { "IdTransacao", "Ano", "created_at", "DataTransacao", "deleted_at", "Descricao", "Dia", "GrupoParcelasId", "IdCartao", "IdCategoria", "IdTransacaoFixa", "Mes", "Parcela", "Status", "Tipo", "updated_at", "UsuarioId", "Valor" },
                values: new object[] { 9, 2025, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Salário", 10, null, null, null, null, 4, null, "AFinalizar", 1, null, "1", 5000.62m });

            migrationBuilder.InsertData(
                table: "TransacoesFixas",
                columns: new[] { "IdTransacaoFixa", "created_at", "DataInicio", "DataTermino", "deleted_at", "Descricao", "IdCartao", "IdCategoria", "Tipo", "updated_at", "UsuarioId", "Valor" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Aluguel", null, null, 0, null, "1", 800m });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "IdCategoria", "created_at", "deleted_at", "Descricao", "Dia", "IdCategoriaPai", "Nome", "updated_at", "UsuarioId" },
                values: new object[] { 3, new DateTime(2025, 7, 6, 14, 8, 55, 988, DateTimeKind.Utc).AddTicks(9009), null, null, 28, 2, "Tv", null, "1" });

            migrationBuilder.InsertData(
                table: "Transacoes",
                columns: new[] { "IdTransacao", "Ano", "created_at", "DataTransacao", "deleted_at", "Descricao", "Dia", "GrupoParcelasId", "IdCartao", "IdCategoria", "IdTransacaoFixa", "Mes", "Parcela", "Status", "Tipo", "updated_at", "UsuarioId", "Valor" },
                values: new object[,]
                {
                    { 1, 2025, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Compra de tomada", 11, null, 1, null, null, 4, null, "AFinalizar", 0, null, "1", 3.5m },
                    { 2, 2025, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Compra mercado", 12, null, null, 1, null, 4, null, "Finalizada", 0, null, "1", 2.25m },
                    { 3, 2025, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Compra mercado", 13, null, null, 1, null, 4, null, "Finalizada", 0, null, "1", 30.75m },
                    { 4, 2025, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Compra mercado Sonda", 14, null, null, 1, null, 4, null, "Vencida", 0, null, "1", 3m },
                    { 8, 2025, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Compra fritadeira", 18, null, null, 2, null, 4, null, "AFinalizar", 0, null, "1", 3m },
                    { 10, 2025, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Job mercado", 10, null, null, 1, null, 4, null, "Finalizada", 1, null, "1", 2.25m },
                    { 11, 2025, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Job mercado 2", 10, null, null, 1, null, 4, null, "Finalizada", 1, null, "1", 3.75m },
                    { 12, 2025, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Job mercado 3", 10, null, null, 1, null, 4, null, "Vencida", 1, null, "1", 3.5m },
                    { 16, 2025, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Job fritadeira", 20, null, null, 2, null, 4, null, "AFinalizar", 1, null, "1", 3.5m },
                    { 17, 2025, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Aluguel", 15, null, null, null, 1, 4, null, "AFinalizar", 0, null, "1", 800m }
                });

            migrationBuilder.InsertData(
                table: "Cartoes",
                columns: new[] { "IdCartao", "Bandeira", "created_at", "deleted_at", "Descricao", "DiaFechamento", "DiaVencimento", "IdCategoria", "Limite", "Nome", "updated_at", "UsuarioId" },
                values: new object[] { 2, "Visa", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 5, 10, 3, 5000m, "Cartão de crédito 2", null, "1" });

            migrationBuilder.InsertData(
                table: "Transacoes",
                columns: new[] { "IdTransacao", "Ano", "created_at", "DataTransacao", "deleted_at", "Descricao", "Dia", "GrupoParcelasId", "IdCartao", "IdCategoria", "IdTransacaoFixa", "Mes", "Parcela", "Status", "Tipo", "updated_at", "UsuarioId", "Valor" },
                values: new object[,]
                {
                    { 5, 2025, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Compra tv", 15, "1", null, 3, null, 4, "1/3", "AFinalizar", 0, null, "1", 3m },
                    { 6, 2025, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Compra tv", 16, "1", null, 3, null, 4, "2/3", "AFinalizar", 0, null, "1", 3m },
                    { 7, 2025, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Compra tv", 17, "1", null, 3, null, 4, "3/3", "AFinalizar", 0, null, "1", 3m },
                    { 13, 2025, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Job tv", 5, "2", 2, null, null, 4, "1/3", "AFinalizar", 1, null, "1", 3.5m },
                    { 14, 2025, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Job tv", 10, "2", 2, null, null, 4, "2/3", "AFinalizar", 1, null, "1", 3.5m },
                    { 15, 2025, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Job tv", 15, "2", 2, null, null, 4, "3/3", "AFinalizar", 1, null, "1", 3000.5m }
                });

            migrationBuilder.InsertData(
                table: "TransacoesFixas",
                columns: new[] { "IdTransacaoFixa", "created_at", "DataInicio", "DataTermino", "deleted_at", "Descricao", "IdCartao", "IdCategoria", "Tipo", "updated_at", "UsuarioId", "Valor" },
                values: new object[] { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Aluguel 2", 2, null, 0, null, "1", 800m });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Balancos_UsuarioId",
                table: "Balancos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Cartoes_IdCategoria",
                table: "Cartoes",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Cartoes_UsuarioId",
                table: "Cartoes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_IdCategoriaPai",
                table: "Categorias",
                column: "IdCategoriaPai");

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_UsuarioId",
                table: "Categorias",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_UsuarioId",
                table: "Tags",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TransacaoFixaTags_TransacaoFixaIdTransacaoFixa",
                table: "TransacaoFixaTags",
                column: "TransacaoFixaIdTransacaoFixa");

            migrationBuilder.CreateIndex(
                name: "IX_TransacaoTags_TransacoesIdTransacao",
                table: "TransacaoTags",
                column: "TransacoesIdTransacao");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_IdCartao",
                table: "Transacoes",
                column: "IdCartao");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_IdCategoria",
                table: "Transacoes",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_IdTransacaoFixa",
                table: "Transacoes",
                column: "IdTransacaoFixa");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_UsuarioId",
                table: "Transacoes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TransacoesFixas_IdCartao",
                table: "TransacoesFixas",
                column: "IdCartao");

            migrationBuilder.CreateIndex(
                name: "IX_TransacoesFixas_IdCategoria",
                table: "TransacoesFixas",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_TransacoesFixas_UsuarioId",
                table: "TransacoesFixas",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Balancos");

            migrationBuilder.DropTable(
                name: "TransacaoFixaTags");

            migrationBuilder.DropTable(
                name: "TransacaoTags");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Transacoes");

            migrationBuilder.DropTable(
                name: "TransacoesFixas");

            migrationBuilder.DropTable(
                name: "Cartoes");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
