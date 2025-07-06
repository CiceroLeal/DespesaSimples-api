using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DespesaSimples_API.Entities;

public sealed class Categoria : BaseEntity
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? IdCategoria { get; set; }
    
    public int? IdCategoriaPai { get; set; }
    
    [MaxLength(60)]
    [Required]
    public required string Nome { get; set; }
    
    [MaxLength(255)]
    public string? Descricao { get; set; }
    
    public int? Dia { get; set; }
    
    public Categoria? CategoriaPai { get; set; }
    
    public ICollection<Categoria> Subcategorias { get; set; } = new List<Categoria>();
    
    public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
    
    public ICollection<Cartao> Cartoes { get; set; } = new List<Cartao>();
    
    [MaxLength(36)]
    public string UsuarioId { get; set; } = string.Empty;
    
    public User? Usuario { get; set; }
    
    public Categoria(){}
    
    [SetsRequiredMembers]
    public Categoria(int idCategoria, 
        int? idCategoriaPai, 
        string nome, 
        string? descricao, 
        int? dia,
        Categoria categoriaPai)
    {
        IdCategoria = idCategoria;
        IdCategoriaPai = idCategoriaPai;
        Nome = nome;
        Descricao = descricao;
        Dia = dia;
        CategoriaPai = categoriaPai;
    }
}