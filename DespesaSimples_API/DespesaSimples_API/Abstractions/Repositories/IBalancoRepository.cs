using DespesaSimples_API.Entities;

namespace DespesaSimples_API.Abstractions.Repositories
{
    public interface IBalancoRepository
    {
        Task<Balanco?> BuscarPorIdAsync(int id);
        Task<Balanco?> BuscarPorAnoMesAsync(int ano, int mes);
        Task<List<Balanco>> BuscarPorAnoAsync(int ano);
        Task<List<Balanco>> BuscarTodosAsync();
        Task<bool> CriarBalancoAsync(Balanco balanco);
        Task<bool> AtualizarBalancosync(Balanco balanco);
        Task<bool> RemoverBalancoAsync(Balanco balanco);
    }
} 