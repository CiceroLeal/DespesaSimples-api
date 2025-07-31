using DespesaSimples_API.Dtos.Balanco;

namespace DespesaSimples_API.Abstractions.Services;

public interface IBalancoService
{
    Task<BalancoDto?> BuscarPorAnoMesAsync(int ano, int mes);
    Task<List<BalancoDto>> BuscarPorAnoAsync(int ano);
    Task<bool> AtualizarBalancoAsync(BalancoDto balanco);
    Task<bool> ReprocessarBalancosAPartirDeAsync(DateTime dataAlterada);
}