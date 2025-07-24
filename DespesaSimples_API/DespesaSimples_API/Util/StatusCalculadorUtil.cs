using DespesaSimples_API.Entities;
using DespesaSimples_API.Enums;

namespace DespesaSimples_API.Util;

public static class StatusCalculadorUtil
{
    public static string? CalculaStatusGrupo(IEnumerable<Transacao> transacoes)
    {
        var statusDistintos = transacoes
            .Select(d => d.Status)
            .Distinct()
            .ToList();
        
        return statusDistintos.Count == 1 ? statusDistintos.First() : null;
    }
    
    public static string CalculaStatus(int dia, int mes, int ano)
    {
        var diaVencimento = dia;
        var ultimoDiaDoMes = DateTime.DaysInMonth(ano, mes);
        if (diaVencimento > ultimoDiaDoMes)
            diaVencimento = ultimoDiaDoMes;

        var dataVencimento = new DateTime(ano, mes, diaVencimento);
        return DateTime.Now.Date > dataVencimento.Date
            ? nameof(StatusTransacaoEnum.Vencida)
            : nameof(StatusTransacaoEnum.AFinalizar);
    }
}