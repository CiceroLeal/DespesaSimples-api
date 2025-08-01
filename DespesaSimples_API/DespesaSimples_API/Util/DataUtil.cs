namespace DespesaSimples_API.Util;

public static class DataUtil
{
    public static DateTime ObterDataReferencia() =>
        new(DateTime.Now.Year, DateTime.Now.Month, 1);

    public static DateTime NormalizarParaInicioDoMes(DateTime data) =>
        new(data.Year, data.Month, 1);
    
    public static int ObterProximoMes(int mes) => 
        mes == 12 ? 1 : mes + 1;
}