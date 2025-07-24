using DespesaSimples_API.Dtos.Transacao;
using MediatR;

namespace DespesaSimples_API.Queries.BuscarTransacaoPorIdFixaMesAno;

public record BuscarTransacaoPorIdFixaMesAnoQuery(int IdTransacaoFixa, int Mes, int Ano) : IRequest<TransacaoDto?>;