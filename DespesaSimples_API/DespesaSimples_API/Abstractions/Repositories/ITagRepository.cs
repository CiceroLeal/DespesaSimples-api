using DespesaSimples_API.Entities;

namespace DespesaSimples_API.Abstractions.Repositories;

public interface ITagRepository
{
    Task<Tag?> BuscarTagPorNomeAsync(string nome);
    Task<List<Tag>> BuscarTagsPorIdsAsync(List<int> ids);
    Task<List<Tag>> BuscarTodasTagsAsync();
    Task<Tag?> BuscarTagPorIdAsync(int id);
    Task<bool> CriarTagAsync(Tag tag);
    Task<bool> RemoverTagAsync(int id);
    Task<bool> AtualizarTagAsync(Tag tag);
    Task<List<Tag>> UpsertTagsAsync(List<string> nomesTags);
} 