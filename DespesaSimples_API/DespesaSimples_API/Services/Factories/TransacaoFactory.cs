using DespesaSimples_API.Abstractions.Services.Factories;
using DespesaSimples_API.Dtos.Transacao;
using DespesaSimples_API.Entities;
using DespesaSimples_API.Mappers;
using DespesaSimples_API.Util;

namespace DespesaSimples_API.Services.Factories;

public class TransacaoFactory : ITransacaoFactory
{
    public List<Transacao> Create(TransacaoCriacaoDto dto, List<Tag> tags)
    {
        if (dto.Parcelas > 0) 
            return GerarTransacoesParceladas(dto, tags);
        
        var transacao = TransacaoMapper.MapCriacaoDtoParaTransacao(dto, tags);
        
        return [transacao];
    }
    
    private static List<Transacao> GerarTransacoesParceladas(TransacaoCriacaoDto dto, List<Tag> tags)
    {
        var transacoes = new List<Transacao>();
        var mes = dto.DataVencimento.Month;
        var ano = dto.DataVencimento.Year;
        var grupoParcelasId = Guid.NewGuid().ToString();
        var primeiraParcela = true;
        
        for (var parcelaAtual = 1; parcelaAtual <= dto.Parcelas; parcelaAtual++)
        {
            var transacao = CriarTransacaoParcela(
                dto, 
                tags, 
                grupoParcelasId, 
                parcelaAtual, 
                dto.Parcelas, 
                mes, 
                ano
            );

            if (primeiraParcela)
            {
                transacao.DataTransacao = dto.DataTransacao;
                primeiraParcela = false;
            }
            
            transacoes.Add(transacao);

            var novoMes = DataUtil.ObterProximoMes(mes);
            if (novoMes < mes)
                ano++;
            mes = novoMes;
        }

        return transacoes;
    }
    
    private static Transacao CriarTransacaoParcela(
        TransacaoCriacaoDto dto, List<Tag> tags, string grupoParcelasId,
        int numeroParcela, int totalParcelas, int mes, int ano)
    {
        var transacao = TransacaoMapper.MapCriacaoDtoParaTransacao(dto, tags);
        
        transacao.Mes = mes;
        transacao.Ano = ano;
        transacao.Parcela = $"{numeroParcela}/{totalParcelas}";
        transacao.GrupoParcelasId = grupoParcelasId;
        transacao.DataTransacao = null;
        
        return transacao;
    }
}