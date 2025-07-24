using DespesaSimples_API.Dtos.TransacaoFixa;

namespace DespesaSimples_API.Abstractions.Services;

public interface ITransacaoFixaService
{
    Task<TransacaoFixaResponseDto> BuscarTransacoesFixasAsync();
    Task<TransacaoFixaResponseDto> BuscarTransacaoFixaPorIdAsync(int id);
    
    Task<bool> CriarTransacaoFixaAsync(TransacaoFixaFormDto dto);
    Task<bool> CriarTransacoesParaMesAnoAsync(int ano, int mes);

    Task<bool> AtualizarTransacaoFixaAsync(
        int id,
        TransacaoFixaFormDto dto,
        bool transacaoAnteriores);

    Task<bool> RemoverTransacaoFixaPorIdAsync(int id, bool transacoesAnteriores);
} 