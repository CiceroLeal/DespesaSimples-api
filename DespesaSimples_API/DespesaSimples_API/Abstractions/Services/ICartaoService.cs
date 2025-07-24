using DespesaSimples_API.Dtos.Cartao;

namespace DespesaSimples_API.Abstractions.Services;

public interface ICartaoService
{
    Task<CartaoResponseDto> BuscarCartoesAsync(int? mes = null, int? ano = null);
    Task<CartaoResponseDto> BuscarCartaoPorIdAsync(string id);
    Task<bool> RemoverCartaoPorIdAsync(string id);
    Task<bool> CriarCartaoAsync(CartaoFormDto cartaoFormDto);
    Task<bool> AtualizarCartaoAsync(string id, CartaoFormDto cartaoFormDto);
}