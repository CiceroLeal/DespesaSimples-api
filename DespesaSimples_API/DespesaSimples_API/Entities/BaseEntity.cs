using System.ComponentModel.DataAnnotations.Schema;

namespace DespesaSimples_API.Entities;

public abstract class BaseEntity
{
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }
} 