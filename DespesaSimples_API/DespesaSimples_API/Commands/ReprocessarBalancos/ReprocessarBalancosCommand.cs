using MediatR;

namespace DespesaSimples_API.Commands.ReprocessarBalancos;

public record ReprocessarBalancosCommand(DateTime DataAlterada) : INotification;