using DespesaSimples_API.Enums;
using MediatR;

namespace DespesaSimples_API.Commands.AtualizarDiaTransacoesFuturas;

public record AtualizarDiaTransacoesFuturasCommand(TipoCategoriaEnum Tipo, int Id, int NovoDia, int AnoAtual,
    int MesAtual) :  IRequest<bool>;