using DespesaSimples_API.Dtos.TransacaoFixa;
using DespesaSimples_API.Enums;

namespace DespesaSimples_API.Abstractions.Services;

public interface ITransacaoFixaService
{
    Task<List<TransacaoFixaDto>> BuscarTransacoesFixasAsync();
    Task<TransacaoFixaDto?> BuscarTransacaoFixaPorIdAsync(int id);
    Task<List<TransacaoFixaDto>> BuscarTransacoesFixasPorMesAnoAsync(int mes, int ano,
        TipoTransacaoEnum? tipo);

    Task<bool> CriarTransacaoFixaAsync(TransacaoFixaFormDto dto);
    Task<bool> CriarTransacoesParaMesAnoAsync(int ano, int mes);

    Task<bool> AtualizarTransacaoFixaAsync(
        int id,
        TransacaoFixaFormDto dto,
        bool transacaoAnteriores);

    Task<bool> RemoverTransacaoFixaPorIdAsync(int id, bool transacoesAnteriores);
}