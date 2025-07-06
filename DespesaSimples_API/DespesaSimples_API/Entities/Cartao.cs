using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DespesaSimples_API.Entities;

public sealed class Cartao : BaseEntity
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? IdCartao { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public required decimal Limite { get; set; }
    
    [MaxLength(60)]
    [Required]
    public required string Nome { get; set; }
    
    [MaxLength(255)]
    public string? Descricao { get; set; }
    
    [MaxLength(60)]
    public string? Bandeira { get; set; }
    
    public int? DiaFechamento { get; set; }
    
    [Required]
    public required int DiaVencimento { get; set; }
    
    [NotMapped]
    public decimal TotalMes { get; set; }
    
    public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
    
    public int? IdCategoria { get; set; }
    
    [ForeignKey("IdCategoria")]
    public Categoria? Categoria { get; set; }
    
    [MaxLength(36)]
    public string UsuarioId { get; set; } = string.Empty;
    
    public User? Usuario { get; set; }
    
    public Cartao(){}
    
    [SetsRequiredMembers]
    public Cartao(int idCartao,
        decimal limite,
        string nome,
        string descricao, 
        string? bandeira, 
        int? idCategoria,
        int? diaFechamento,
        int diaVencimento)
    {
        IdCartao = idCartao;
        Limite = limite;
        Descricao = descricao;
        Nome = nome;
        Bandeira = bandeira;
        IdCategoria = idCategoria;
        DiaFechamento = diaFechamento;
        DiaVencimento = diaVencimento;
    }
}