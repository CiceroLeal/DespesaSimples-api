using DespesaSimples_API.Dtos;
using DespesaSimples_API.Dtos.Responses;

namespace DespesaSimples_API.Abstractions.Services;

public interface IBalancoService
{
    Task<BalancoResponseDto> ObterPorAnoMesAsync(int ano, int mes);
    Task<BalancoResponseDto> ObterPorAnoAsync(int ano);
    Task<bool> AtualizarBalancoAsync(BalancoDto balanco);
    Task<bool> ReprocessarBalancosAPartirDeAsync(DateTime dataAlterada);
}