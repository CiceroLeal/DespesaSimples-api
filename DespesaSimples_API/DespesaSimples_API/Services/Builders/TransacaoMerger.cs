using DespesaSimples_API.Dtos.Transacao;
using DespesaSimples_API.Util;

namespace DespesaSimples_API.Services.Builders;

public class TransacaoMerger
{
    public List<TransacaoDto> Merge(List<TransacaoDto> variaveisDto, List<TransacaoDto> fixasDto)
    {
        var resultado = new List<TransacaoDto>(variaveisDto);
        var fixasRestantes = new List<TransacaoDto>();

        foreach (var fixa in fixasDto)
        {
            var transacaoExistente = EncontrarTransacaoPorIdRecursivamente(resultado, fixa.IdTransacao);
            
            if (transacaoExistente != null)
            {
                MergearTransacoes(transacaoExistente, fixa);
            }
            else
            {
                fixasRestantes.Add(fixa);
            }
        }

        resultado.AddRange(fixasRestantes);
        return resultado;
    }

    private static TransacaoDto? EncontrarTransacaoPorIdRecursivamente(List<TransacaoDto> transacoes, string idTransacao)
    {
        foreach (var transacao in transacoes)
        {
            if (transacao.IdTransacao == idTransacao)
                return transacao;

            var encontrada = EncontrarTransacaoPorIdRecursivamente(transacao.SubTransacoes, idTransacao);
            if (encontrada != null)
                return encontrada;
        }

        return null;
    }

    private void MergearTransacoes(TransacaoDto destino, TransacaoDto origem)
    {
        foreach (var subOrigem in origem.SubTransacoes)
        {
            var subExistente = EncontrarTransacaoPorIdRecursivamente(destino.SubTransacoes, subOrigem.IdTransacao);
            
            if (subExistente != null)
            {
                MergearTransacoes(subExistente, subOrigem);
            }
            else
            {
                destino.SubTransacoes.Add(subOrigem);
            }
        }
        
        MergearPropriedades(destino, origem);
        RecalcularValor(destino);
    }

    private void MergearPropriedades(TransacaoDto destino, TransacaoDto origem)
    {
        if (destino.Valor == 0 && origem.Valor != 0)
            destino.Valor = origem.Valor;
        
        if (string.IsNullOrEmpty(destino.Descricao) && !string.IsNullOrEmpty(origem.Descricao))
            destino.Descricao = origem.Descricao;
        
        if (destino.Status == null && origem.Status != null)
            destino.Status = origem.Status;
        
        MergearTags(destino, origem);
    }

    private static void MergearTags(TransacaoDto destino, TransacaoDto origem)
    {
        if (destino.Tags == null && origem.Tags != null)
            destino.Tags = origem.Tags;
        else if (destino.Tags != null && origem.Tags != null)
            destino.Tags = destino.Tags.Union(origem.Tags).ToList();
    }

    private static void RecalcularValor(TransacaoDto transacao)
    {
        transacao.Valor = transacao.SubTransacoes.Sum(TransacaoUtil.ValorEfetivoTransacao);
    }
} 