using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using DespesaSimples_API.Abstractions.Entities;
using DespesaSimples_API.Enums;

namespace DespesaSimples_API.Entities;

public sealed class Transacao : BaseEntity, ITemTags
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdTransacao { get; set; }

    [Required]
    [MaxLength(255)]
    public string? Descricao { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public required decimal Valor { get; set; }
    
    public int? Dia { get; set; }

    [Required]
    public required int Ano { get; set; }

    [Required]
    public required int Mes { get; set; }

    public DateTime? DataTransacao { get; set; }
    
    [MaxLength(30)]
    public string? Parcela { get; set; }

    [Required]
    [MaxLength(30)]
    public string? Status { get; set; }

    public int? IdCategoria { get; set; }
    
    public int? IdCartao { get; set; }
    
    public int? IdTransacaoFixa { get; set; }

    [ForeignKey("IdCategoria")]
    public Categoria? Categoria { get; set; }
    
    [ForeignKey("IdCartao")]
    public Cartao? Cartao { get; set; }
    
    [ForeignKey("IdTransacaoFixa")]
    public TransacaoFixa? TransacaoFixa { get; set; }
    
    [MaxLength(36)]
    public string? GrupoParcelasId { get; set; }

    [Required]
    public required TipoTransacaoEnum Tipo { get; set; }

    public ICollection<Tag>? Tags { get; set; } = new List<Tag>();
    
    [MaxLength(36)]
    public string UsuarioId { get; set; } = string.Empty;
    
    public User? Usuario { get; set; }
    
    [SetsRequiredMembers]
    public Transacao(){}

    [SetsRequiredMembers]
    public Transacao(int idTransacao, 
        string descricao, 
        decimal valor, 
        int? dia, 
        int ano, 
        int mes,
        DateTime? dataTransacao,
        string? parcela, 
        string status, 
        int? idCategoria,
        int? idCartao,
        int? idTransacaoFixa,
        string? grupoParcelasId,
        TipoTransacaoEnum tipo)
    {
        IdTransacao = idTransacao;
        Descricao = descricao;
        Valor = valor;
        Dia = dia;
        Ano = ano;
        Mes = mes;
        DataTransacao = dataTransacao;
        Parcela = parcela;
        Status = status;
        GrupoParcelasId = grupoParcelasId;
        IdCategoria = idCategoria;
        IdCartao = idCartao;
        IdTransacaoFixa = idTransacaoFixa;
        Tipo = tipo;
    }
} 