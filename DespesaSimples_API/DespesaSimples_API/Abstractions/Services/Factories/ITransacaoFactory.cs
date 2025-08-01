using DespesaSimples_API.Dtos.Transacao;
using DespesaSimples_API.Entities;

namespace DespesaSimples_API.Abstractions.Services.Factories;

public interface ITransacaoFactory
{
    List<Transacao> Create(TransacaoCriacaoDto dto, List<Tag> tags);
}