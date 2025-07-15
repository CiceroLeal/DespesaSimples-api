namespace DespesaSimples_API.Dtos.Balanco;

public class BalancoDto
{
    public int IdBalanco { get; set; }

    public int Ano { get; set; }

    public int Mes { get; set; }

    public decimal SaldoInicial { get; set; }

    public decimal TotalReceitas { get; set; }

    public decimal TotalDespesas { get; set; }

    public decimal SaldoFinal { get; set; }

    public BalancoDto() { }

    public BalancoDto(int ano, int mes, decimal saldoInicial, decimal totalReceitas, decimal totalDespesas)
    {
        Ano = ano;
        Mes = mes;
        SaldoInicial = saldoInicial;
        TotalReceitas = totalReceitas;
        TotalDespesas = totalDespesas;
        SaldoFinal = saldoInicial + totalReceitas - totalDespesas;
    }
}