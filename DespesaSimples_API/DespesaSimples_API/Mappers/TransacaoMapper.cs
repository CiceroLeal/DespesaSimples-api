using DespesaSimples_API.Dtos.Transacao;
using DespesaSimples_API.Entities;
using DespesaSimples_API.Enums;
using DespesaSimples_API.Util;

namespace DespesaSimples_API.Mappers;

public static class TransacaoMapper
{
    public static TransacaoDto MapParaDto(Transacao transacao)
    {
        return new TransacaoDto
        {
            IdTransacao = transacao.IdTransacao == 0
                ? $"{transacao.IdTransacaoFixa ?? 0}F"
                : transacao.IdTransacao.ToString(),
            Descricao = transacao.Descricao,
            Valor = transacao.Valor,
            Dia = transacao.Dia ?? 0,
            Ano = transacao.Ano,
            Mes = transacao.Mes,
            DataTransacao = transacao.DataTransacao,
            Parcela = transacao.Parcela,
            Status = transacao.Status,
            IdCategoria = transacao.IdCategoria,
            IdCartao = transacao.IdCartao,
            Tags = (transacao.Tags ?? new List<Tag>()).Select(tag => tag.Nome).ToList(),
            Tipo = transacao.Tipo,
            SubTransacoes = []
        };
    }

    public static Transacao MapDtoParaTransacao(TransacaoDto dto, List<Tag> tags)
    {
        var idTransacaoFixa = dto.IdTransacao.EndsWith('F')
            ? int.Parse(dto.IdTransacao[..^1])
            : (int?)null;

        return new Transacao
        {
            Descricao = dto.Descricao ?? "",
            Valor = dto.Valor,
            Dia = dto.Dia,
            Mes = dto.Mes,
            Ano = dto.Ano,
            DataTransacao = dto.DataTransacao,
            Tipo = dto.Tipo ?? 0,
            Status = dto.Status ?? string.Empty,
            IdCategoria = dto.IdCategoria,
            IdCartao = dto.IdCartao,
            IdTransacaoFixa = idTransacaoFixa,
            Tags = tags
        };
    }
    
    public static Transacao MapCriacaoDtoParaTransacao(TransacaoCriacaoDto dto, List<Tag> tags)
    {
        return new Transacao
        {
            Descricao = dto.Descricao,
            Valor = dto.Valor,
            Dia = dto.DataVencimento.Day,
            Mes = dto.DataVencimento.Month,
            Ano = dto.DataVencimento.Year,
            DataTransacao = dto.DataTransacao,
            Tipo = dto.Tipo,
            Status = dto.Finalizada
                ? nameof(StatusTransacaoEnum.Finalizada)
                : StatusCalculadorUtil.CalculaStatus(
                    dto.DataVencimento.Day,
                    dto.DataVencimento.Month,
                    dto.DataVencimento.Year),
            IdCategoria = IdUtil.ParseIdToInt(dto.Categoria, (char)TipoCategoriaEnum.Categoria),
            IdCartao = IdUtil.ParseIdToInt(dto.Cartao, (char)TipoCategoriaEnum.Cartao),
            Tags = tags
        };
    }
    
    public static void MapAtualizacaoDtoParaEntidade(TransacaoAtualizacaoDto dto, Transacao entidade, List<Tag> tags)
    {
        entidade.Valor = dto.Valor;
        entidade.Descricao = dto.Descricao;
        entidade.Dia = dto.DataVencimento.Day;
         
        // Apenas o dia é propagado para as parcelas. Mês e Ano são mantidos.
        if (entidade.GrupoParcelasId == null)
        {
            entidade.Mes = dto.DataVencimento.Month;
            entidade.Ano = dto.DataVencimento.Year;
        }
         
        entidade.DataTransacao = dto.DataTransacao;
        entidade.IdCategoria = IdUtil.ParseIdToInt(dto.Categoria, (char)TipoCategoriaEnum.Categoria);
        entidade.IdCartao = IdUtil.ParseIdToInt(dto.Cartao, (char)TipoCategoriaEnum.Cartao);
        entidade.Status = dto.Finalizada 
            ? nameof(StatusTransacaoEnum.Finalizada) 
            : StatusCalculadorUtil.CalculaStatus(entidade.Dia.Value, entidade.Mes, entidade.Ano);
        entidade.Tags = tags;
    }
    
    public static void MapFuturaAtualizacaoDtoParaEntidade(TransacaoFuturaAtualizacaoDto dto, TransacaoFixa transacaoFixa, Transacao entidade, List<Tag> tags)
    {
        entidade.IdTransacaoFixa = transacaoFixa.IdTransacaoFixa;
        entidade.Tipo = transacaoFixa.Tipo;
        entidade.Descricao = dto.Descricao;
        entidade.Valor = dto.Valor;
        entidade.Dia = dto.DataVencimento.Day;
        entidade.Mes = dto.DataVencimento.Month;
        entidade.Ano = dto.DataVencimento.Year;
        entidade.DataTransacao = dto.DataTransacao;
        entidade.Tags = tags;
 
        var idCategoria = IdUtil.ParseIdToInt(dto.Categoria, (char)TipoCategoriaEnum.Categoria);
        var idCartao = IdUtil.ParseIdToInt(dto.Cartao, (char)TipoCategoriaEnum.Cartao);
 
        entidade.IdCategoria = idCategoria ?? transacaoFixa.IdCategoria;
        entidade.IdCartao = idCartao ?? transacaoFixa.IdCartao;
 
        entidade.Status = dto.Finalizada ? nameof(StatusTransacaoEnum.Finalizada) :
            StatusCalculadorUtil.CalculaStatus(entidade.Dia.Value, entidade.Mes, entidade.Ano);
    }
}