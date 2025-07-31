using DespesaSimples_API.Entities;
using DespesaSimples_API.Enums;

namespace DespesaSimples_API.Abstractions.Repositories;

public interface ITransacaoRepository
{
    Task<List<Transacao>> BuscarTransacoesPorMesAnoAsync(int? ano = null, int? mes = null, TipoTransacaoEnum? tipo = null);
    Task<Transacao?> BuscarTransacaoPorIdAsync(int id);
    Task<Transacao?> BuscarTransacaoPorIdFixaMesAnoAsync(int idTransacaoFixa, int mes, int ano);
    Task<Transacao?> BuscarUltimaTransacaoPorIdFixaAsync(int id);
    Task<List<Transacao>> BuscarTransacaoPorIdFixaAsync(int idTransacaoFixa);
    Task<List<Transacao>> BuscarTransacoesDeletadasPorMesAnoAsync(int ano, int mes, TipoTransacaoEnum? tipo = null);
    Task<decimal> SomarPorTipoAsync(TipoTransacaoEnum tipo, int ano, int mes);
    
    Task<bool> CriarTransacaoAsync(Transacao transacao);
    
    Task<bool> AtualizarTransacaoAsync(Transacao transacao);
    Task<bool> AtualizarDiaTransacoesFuturasPorCategoriaAsync(int idCategoria, int novoDia, int anoAtual, int mesAtual);
    Task<bool> AtualizarDiaTransacoesFuturasPorCartaoAsync(int idCartao, int novoDia, int anoAtual, int mesAtual);
    
    Task<bool> RemoverTransacaoAsync(int id);
    Task<bool> RemoverTransacoesPorIdTransacaoFixaAsync(int idTransacaoFixa);
}