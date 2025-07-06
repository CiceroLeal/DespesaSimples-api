using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DespesaSimples_API.Entities;

public sealed class Balanco : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdBalanco { get; set; }

    [Required]
    public int Ano { get; set; }

    [Required]
    public int Mes { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal SaldoInicial { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal TotalReceitas { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal TotalDespesas { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal SaldoFinal { get; set; }

    [MaxLength(36)]
    [Required]
    public string UsuarioId { get; set; } = string.Empty;

    public User? Usuario { get; set; }

    public Balanco() { }

    public Balanco(int ano, int mes, decimal saldoInicial, decimal totalReceitas, decimal totalDespesas)
    {
        Ano = ano;
        Mes = mes;
        SaldoInicial = saldoInicial;
        TotalReceitas = totalReceitas;
        TotalDespesas = totalDespesas;
        SaldoFinal = saldoInicial + totalReceitas - totalDespesas;
    }
}