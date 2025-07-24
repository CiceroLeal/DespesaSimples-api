using DespesaSimples_API.Dtos.Transacao;
using DespesaSimples_API.Dtos.TransacaoFixa;
using DespesaSimples_API.Entities;
using DespesaSimples_API.Enums;
using DespesaSimples_API.Util;

namespace DespesaSimples_API.Mappers;

public static class TransacaoFixaMapper
{
    public static TransacaoFixaDto MapParaDto(TransacaoFixa transacaoFixa)
    {
        return new TransacaoFixaDto
        {
            IdTransacaoFixa = transacaoFixa.IdTransacaoFixa,
            Descricao = transacaoFixa.Descricao,
            Valor = transacaoFixa.Valor,
            DataInicio = transacaoFixa.DataInicio,
            DataTermino = transacaoFixa.DataTermino,
            Tipo = transacaoFixa.Tipo,
            Tags = (transacaoFixa.Tags ?? new List<Tag>()).Select(tag => tag.Nome).ToList(),
            IdCategoria = transacaoFixa.IdCategoria,
            IdCartao = transacaoFixa.IdCartao,
            Categoria = transacaoFixa.Categoria != null
                ? CategoriaMapper.MapCategoriaParaDto(transacaoFixa.Categoria)
                : null,
            Cartao = transacaoFixa.Cartao != null
                ? CartaoMapper.MapCartaoParaDto(transacaoFixa.Cartao)
                : null
        };
    }

    public static TransacaoFixa MapTransacaoFixaFormDtoParaTransacaoFixa(
        TransacaoFixaFormDto transacaoFixaDto,
        List<Tag>? tags = null)
    {
        var idCategoria = IdUtil.ParseIdToInt(transacaoFixaDto.Categoria, (char)TipoCategoriaEnum.Categoria);
        var idCartao = IdUtil.ParseIdToInt(transacaoFixaDto.Cartao, (char)TipoCategoriaEnum.Cartao);

        return new TransacaoFixa
        {
            Valor = transacaoFixaDto.Valor,
            Descricao = transacaoFixaDto.Descricao,
            DataInicio = transacaoFixaDto.DataInicio,
            DataTermino = transacaoFixaDto.DataTermino,
            IdCategoria = idCategoria,
            IdCartao = idCartao,
            Tipo = transacaoFixaDto.Tipo,
            Tags = tags ?? []
        };
    }

    public static TransacaoDto MapTransacaoFixaParaTransacaoDto(
        TransacaoFixa transacaoFixa,
        bool finalizada = false)
    {
        return new TransacaoDto
        {
            IdTransacao = $"{transacaoFixa.IdTransacaoFixa}F",
            Descricao = transacaoFixa.Descricao,
            Valor = transacaoFixa.Valor,
            Dia = transacaoFixa.DataInicio.Day,
            Ano = transacaoFixa.DataInicio.Year,
            Mes = transacaoFixa.DataInicio.Month,

            Status = finalizada
                ? nameof(StatusTransacaoEnum.Finalizada)
                : StatusCalculadorUtil.CalculaStatus(
                    transacaoFixa.DataInicio.Day,
                    transacaoFixa.DataInicio.Month,
                    transacaoFixa.DataInicio.Year),

            IdCategoria = transacaoFixa.IdCategoria,
            Categoria = transacaoFixa.Categoria != null
                ? CategoriaMapper.MapCategoriaParaDto(transacaoFixa.Categoria)
                : null,
            IdCartao = transacaoFixa.IdCartao,
            Cartao = transacaoFixa.Cartao != null
                ? CartaoMapper.MapCartaoParaDto(transacaoFixa.Cartao)
                : null,
            Tags = transacaoFixa.Tags?.Select(t => t.Nome).ToList(),
            Tipo = transacaoFixa.Tipo,
            SubTransacoes = []
        };
    }
}