using DespesaSimples_API.Dtos.Transacao;
using DespesaSimples_API.Entities;
using DespesaSimples_API.Mappers;
using DespesaSimples_API.Mappers.Grouping;
using DespesaSimples_API.Util;

namespace DespesaSimples_API.Services.Builders;

public class TransacaoDtoBuilder(IEnumerable<Transacao> transacoes)
{
    private readonly List<Transacao> _transacoes = transacoes.ToList();
    private readonly Dictionary<string, TransacaoDto> _nodeMap = new();
    private readonly List<TransacaoDto> _rootNodes = [];

    public List<TransacaoDto> Build()
    {
        var transacoesSemGrupo = new List<Transacao>(_transacoes);

        ProcessarCategorias(transacoesSemGrupo);
        ProcessarCartoes(transacoesSemGrupo);
        ProcessarTransacoesRestantes(transacoesSemGrupo);
        
        TransacaoUtil.CalcularValoresHierarquicos(_rootNodes);

        return _rootNodes;
    }

    private void ProcessarCategorias(List<Transacao> transacoesDisponiveis)
    {
        var categorias = _transacoes
            .Where(t => t.Categoria != null)
            .Select(t => t.Categoria!)
            .DistinctBy(c => c.IdCategoria)
            .OrderBy(c => c.IdCategoriaPai.HasValue ? 1 : 0)
            .ToList();

        foreach (var categoria in categorias)
        {
            var transacoesDaCategoria = _transacoes
                .Where(t => t.IdCategoria == categoria.IdCategoria && t.IdCartao == null)
                .ToList();
            
            var dto = CategoriaDtoMapper.MapParaTransacaoDto(categoria, transacoesDaCategoria);
            
            var parentId = categoria.IdCategoriaPai.HasValue ? $"{categoria.IdCategoriaPai}C" : null;
            AdicionarNode(dto, parentId);
            
            transacoesDisponiveis.RemoveAll(t => transacoesDaCategoria.Contains(t));
        }
    }

    private void ProcessarCartoes(List<Transacao> transacoesDisponiveis)
    {
        var cartoes = _transacoes
            .Where(t => t.Cartao != null)
            .Select(t => t.Cartao!)
            .DistinctBy(c => c.IdCartao)
            .ToList();

        foreach (var cartao in cartoes)
        {
            var transacoesDoCartao = _transacoes.Where(t => t.IdCartao == cartao.IdCartao).ToList();
            var dto = CartaoDtoMapper.MapParaTransacaoDto(cartao, transacoesDoCartao);
            
            var parentId = cartao.IdCategoria.HasValue ? $"{cartao.IdCategoria}C" : null;
            AdicionarNode(dto, parentId);

            transacoesDisponiveis.RemoveAll(t => transacoesDoCartao.Contains(t));
        }
    }

    private void ProcessarTransacoesRestantes(List<Transacao> transacoesRestantes)
    {
        foreach (var dto in transacoesRestantes.Select(TransacaoMapper.MapParaDto))
        {
            _rootNodes.Add(dto);
        }
    }

    private void AdicionarNode(TransacaoDto node, string? parentId)
    {
        _nodeMap[node.IdTransacao] = node;
        
        if (parentId != null && _nodeMap.TryGetValue(parentId, out var parentNode))
        {
            parentNode.SubTransacoes.Add(node);
        }
        else
        {
            _rootNodes.Add(node);
        }
    }
}