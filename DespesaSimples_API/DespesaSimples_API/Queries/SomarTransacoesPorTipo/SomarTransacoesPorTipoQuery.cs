using DespesaSimples_API.Enums;
using MediatR;

namespace DespesaSimples_API.Queries.SomarTransacoesPorTipo;

public record SomarTransacoesPorTipoQuery(
    TipoTransacaoEnum TipoTransacao,
    int Ano,
    int Mes
) : IRequest<decimal>;