using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DespesaSimples_API.Entities;

public sealed class User : IdentityUser
{
    [MaxLength(255)]
    public string Nome { get; set; } = string.Empty;
    public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
    public ICollection<TransacaoFixa> TransacoesFixa { get; set; } = new List<TransacaoFixa>();
    public ICollection<Categoria> Categorias { get; set; } = new List<Categoria>();
    public ICollection<Cartao> Cartoes { get; set; } = new List<Cartao>();
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public ICollection<Balanco> Balancos { get; set; } = new List<Balanco>();
    
} 