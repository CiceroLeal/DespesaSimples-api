using DespesaSimples_API.Entities;

namespace DespesaSimples_API.Abstractions.Repositories;

public interface ICartaoRepository
{
    Task<List<Cartao>> BuscarCartoesAsync();
    Task<List<Cartao>> BuscarCartoesComGastosDoMesAsync(int mes, int ano);
    Task<Cartao?> BuscarCartaoPorIdAsync(int id);
    Task<bool> RemoverCartaoAsync(int id);
    Task<bool> CriarCartaoAsync(Cartao cartao);
    Task<bool> AtualizarCartaoAsync(Cartao cartao);
} 