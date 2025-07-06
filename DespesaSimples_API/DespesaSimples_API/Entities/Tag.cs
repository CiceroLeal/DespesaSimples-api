using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DespesaSimples_API.Entities;

public sealed class Tag : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdTag { get; set; }
    
    [Required]
    [MaxLength(60)]
    public required string Nome { get; set; }
    
    public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
    
    [MaxLength(36)]
    public string UsuarioId { get; set; } = string.Empty;
    
    public User? Usuario { get; set; }
} 