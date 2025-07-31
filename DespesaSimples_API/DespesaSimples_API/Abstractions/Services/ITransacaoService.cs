using DespesaSimples_API.Dtos.Transacao;
using DespesaSimples_API.Entities;
using DespesaSimples_API.Enums;

namespace DespesaSimples_API.Abstractions.Services;

public interface ITransacaoService
{
    Task<List<TransacaoDto>> BuscarTransacoesAsync(int? ano, int? mes, TipoTransacaoEnum? tipo, List<string> tags);
    Task<TransacaoDto?> BuscarTransacaoPorIdAsync(int id);
    Task<TransacaoDto?> BuscarTransacaoPorIdFixaMesAnoAsync(int idTransacaoFixa, int mes, int ano);
    Task<TransacaoDto?> BuscarUltimaTransacaoPorIdFixaAsync(int idTransacaoFixa);
    Task<TransacaoDto?> BuscarTransacaoPorIdTransacaoFixaAsync(string id, int mes, int ano);
    Task<decimal> SomarPorTipoAsync(TipoTransacaoEnum tipo, int ano, int mes);

    Task<bool> CriarTransacaoAPartirDaFixaAsync(TransacaoDto dto);
    Task<bool> CriarTransacoesAPartirDaFixaAsync(TransacaoDto transacaoDto, DateTime? dataTermino, List<Tag> tags);

    Task<bool> AtualizarTransacaoAPartirDaFixaAsync(int id, TransacaoDto dto, List<Tag> tags);
    Task<bool> AtualizarDiaTransacoesFuturasAsync(TipoCategoriaEnum tipo, int idCategoria, int novoDia, int anoAtual,
        int mesAtual);
    Task<bool> AtualizarTransacoesAPartirDaFixaAsync(int idTransacaoFixa, TransacaoDto dto, List<Tag> tags);

    Task<bool> RemoverTransacaoPorIdAsync(int id);
    Task<bool> RemoverTransacoesPorIdTransacaoFixaAsync(int idTransacaoFixa);
}