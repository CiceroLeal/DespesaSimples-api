using DespesaSimples_API.Entities;

namespace DespesaSimples_API.Abstractions.Repositories
{
    public interface IBalancoRepository
    {
        Task<Balanco?> ObterPorIdAsync(int id);
        Task<Balanco?> ObterPorAnoMesAsync(int ano, int mes);
        Task<List<Balanco>> ObterPorAnoAsync(int ano);
        Task<List<Balanco>> ObterTodosAsync();
        Task<bool> CriarBalancoAsync(Balanco balanco);
        Task<bool> AtualizarBalancosync(Balanco balanco);
        Task<bool> RemoverBalancoAsync(Balanco balanco);
    }
} 