using DespesaSimples_API.Entities;

namespace DespesaSimples_API.Abstractions.Repositories;

public interface ITagRepository
{
    Task<Tag?> ObterTagPorNomeAsync(string nome);
    Task<List<Tag>> ObterTagsPorIdsAsync(List<int> ids);
    Task<List<Tag>> ObterTodasTagsAsync();
    Task<Tag?> ObterTagPorIdAsync(int id);
    Task<bool> CriarTagAsync(Tag tag);
    Task<bool> RemoverTagAsync(int id);
    Task<bool> AtualizarTagAsync(Tag tag);
} 