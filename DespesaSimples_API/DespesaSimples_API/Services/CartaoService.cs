using DespesaSimples_API.Abstractions.Repositories;
using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Commands.AtualizarDiaTransacoesFuturas;
using DespesaSimples_API.Dtos.Cartao;
using DespesaSimples_API.Entities;
using DespesaSimples_API.Enums;
using DespesaSimples_API.Exceptions;
using DespesaSimples_API.Mappers;
using DespesaSimples_API.Util;
using MediatR;

namespace DespesaSimples_API.Services;

public class CartaoService(ICartaoRepository cartaoRepository, IMediator mediator)
    : ICartaoService
{
    public async Task<CartaoResponseDto> ObterCartoesAsync(int? mes = null, int? ano = null)
    {
        List<Cartao> cartoes;

        if (mes != null && ano != null)
            cartoes = await cartaoRepository.BuscarCartoesComGastosDoMesAsync(mes.Value, ano.Value);
        else
            cartoes = await cartaoRepository.BuscarCartoesAsync();

        return new CartaoResponseDto
        {
            Cartoes = cartoes.Select(CartaoMapper.MapCartaoParaDto).ToList()
        };
    }

    public async Task<CartaoResponseDto> ObterCartaoPorIdAsync(string id)
    {
        var idInt = IdUtil.ParseIdToInt(id, (char)TipoCategoriaEnum.Cartao);
        var cartao = await cartaoRepository.ObterCartaoPorIdAsync(idInt ?? 0);

        return new CartaoResponseDto
        {
            Cartoes = cartao != null ? [CartaoMapper.MapCartaoParaDto(cartao)] : []
        };
    }

    public async Task<bool> RemoverCartaoPorIdAsync(string id)
    {
        var idInt = IdUtil.ParseIdToInt(id, (char)TipoCategoriaEnum.Cartao) ?? 0;
        return await cartaoRepository.RemoverCartaoAsync(idInt);
    }

    public async Task<bool> CriarCartaoAsync(CartaoFormDto cartaoCriacaoDto)
    {
        ArgumentNullException.ThrowIfNull(cartaoCriacaoDto);

        var cartao = CartaoMapper.MapCartaoCriacaoDtoParaCartao(cartaoCriacaoDto);

        return await cartaoRepository.CriarCartaoAsync(cartao);
    }

    public async Task<bool> AtualizarCartaoAsync(string id, CartaoFormDto cartaoFormDto)
    {
        var idInt = IdUtil.ParseIdToInt(id, (char)TipoCategoriaEnum.Cartao);

        var cartao = await cartaoRepository.ObterCartaoPorIdAsync(idInt ?? 0);

        if (cartao == null)
            throw new NotFoundException();

        var diaAnterior = cartao.DiaVencimento;
        cartao.Limite = cartaoFormDto.Limite;
        cartao.Nome = cartaoFormDto.Nome;
        cartao.Descricao = cartaoFormDto.Descricao;
        cartao.Bandeira = cartaoFormDto.Bandeira;
        cartao.DiaFechamento = cartaoFormDto.DiaFechamento;
        cartao.DiaVencimento = cartaoFormDto.DiaVencimento;
        cartao.IdCategoria = IdUtil.ParseIdToInt(cartaoFormDto.IdCategoria, (char)TipoCategoriaEnum.Categoria);

        var result = await cartaoRepository.AtualizarCartaoAsync(cartao);

        if (!result || diaAnterior == cartaoFormDto.DiaVencimento)
            return result;

        // Dia de vencimento do Cartão foi alterado, atualizar transações futuras caso existam
        var dataAtual = DateTime.Now;

        await mediator.Send(new AtualizarDiaTransacoesFuturasCommand(TipoCategoriaEnum.Cartao,
            idInt ?? 0,
            cartaoFormDto.DiaVencimento,
            dataAtual.Year,
            dataAtual.Month));

        return result;
    }
}