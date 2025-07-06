using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using DespesaSimples_API.Abstractions.Entities;
using DespesaSimples_API.Enums;

namespace DespesaSimples_API.Entities;

public sealed class TransacaoFixa : BaseEntity, ITemTags
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? IdTransacaoFixa { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public required decimal Valor { get; set; }
    
    [Required]
    [MaxLength(255)]
    public required string Descricao { get; set; }
    
    [Required]
    public required DateTime DataInicio { get; set; }
    
    public DateTime? DataTermino { get; set; }
    
    public int? IdCategoria { get; set; }
    public int? IdCartao { get; set; }
    
    [Required]
    public required TipoTransacaoEnum Tipo { get; set; }
    
    [ForeignKey("IdCategoria")]
    public Categoria? Categoria { get; set; }
    
    [ForeignKey("IdCartao")]
    public Cartao? Cartao { get; set; }

    public ICollection<Tag>? Tags { get; set; } = new List<Tag>();
    
    [MaxLength(36)]
    public string UsuarioId { get; set; } = string.Empty;
    
    public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
    
    public User? Usuario { get; set; }
    
    public TransacaoFixa(){}
    
    [SetsRequiredMembers]
    public TransacaoFixa(int? idTransacaoFixa, 
        decimal valor, 
        string descricao, 
        DateTime dataInicio, 
        DateTime? dataTermino, 
        int? idCategoria,
        int? idCartao,
        TipoTransacaoEnum tipo)
    {
        IdTransacaoFixa = idTransacaoFixa;
        Valor = valor;
        Descricao = descricao;
        DataInicio = dataInicio;
        DataTermino = dataTermino;
        IdCategoria = idCategoria;
        IdCartao = idCartao;
        Tipo = tipo;
    }
} 