using DespesaSimples_API.Entities;

namespace DespesaSimples_API.Abstractions.Entities;

public interface ITemTags
{
    public ICollection<Tag>? Tags { get; set; }
}