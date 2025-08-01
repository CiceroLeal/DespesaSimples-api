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

    public static TransacaoFixa MapFixaFormDtoParaFixa(
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

    public static TransacaoDto MapFixaParaDto(
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
    
    public static List<TransacaoDto> MapFixasParaDto(List<TransacaoFixaDto> fixas, int mes, int ano)
    {
        return fixas
            .Select(dto =>
            {
                dto.DataInicio = new DateTime(ano, mes, dto.DataInicio.Day);
                return dto;
            })
            .Select(MapFixaDtoParaDto)
            .ToList();
    }
    
    public static TransacaoDto MapFixaDtoParaDto(TransacaoFixaDto transacaoFixa)
    {
        return new TransacaoDto
        {
            IdTransacao = $"{transacaoFixa.IdTransacaoFixa ?? 0}F",
            Descricao = transacaoFixa.Descricao,
            Valor = transacaoFixa.Valor,
            Dia = transacaoFixa.DataInicio.Day,
            Ano = transacaoFixa.DataInicio.Year,
            Mes = transacaoFixa.DataInicio.Month,

            Status = transacaoFixa.Finalizada == null ? nameof(StatusTransacaoEnum.AFinalizar) :
                (bool)transacaoFixa.Finalizada ? nameof(StatusTransacaoEnum.Finalizada) :
                StatusCalculadorUtil.CalculaStatus(
                    transacaoFixa.DataInicio.Day,
                    transacaoFixa.DataInicio.Month,
                    transacaoFixa.DataInicio.Year),

            IdCategoria = transacaoFixa.IdCategoria,
            Categoria = transacaoFixa.Categoria,
            IdCartao = transacaoFixa.IdCartao,
            Cartao = transacaoFixa.Cartao,
            Tags = transacaoFixa.Tags,
            Tipo = transacaoFixa.Tipo,
            SubTransacoes = []
        };
    }
    
    public static TransacaoFixa MapTransacaoCriacaoDtoParaTransacaoFixa(TransacaoCriacaoDto dto)
    {
        return new TransacaoFixa
        {
            Valor = dto.Valor,
            Descricao = dto.Descricao,
            DataInicio = dto.DataVencimento,
            DataTermino = dto.DataTermino,
            IdCategoria = IdUtil.ParseIdToInt(dto.Categoria, (char)TipoCategoriaEnum.Categoria),
            IdCartao = IdUtil.ParseIdToInt(dto.Cartao, (char)TipoCategoriaEnum.Cartao),
            Tipo = dto.Tipo,
        };
    }
}