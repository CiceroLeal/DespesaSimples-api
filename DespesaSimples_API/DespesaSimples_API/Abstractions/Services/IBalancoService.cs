using DespesaSimples_API.Dtos;
using DespesaSimples_API.Dtos.Balanco;

namespace DespesaSimples_API.Abstractions.Services;

public interface IBalancoService
{
    Task<BalancoResponseDto> BuscarPorAnoMesAsync(int ano, int mes);
    Task<BalancoResponseDto> BuscarPorAnoAsync(int ano);
    Task<bool> AtualizarBalancoAsync(BalancoDto balanco);
    Task<bool> ReprocessarBalancosAPartirDeAsync(DateTime dataAlterada);
}