using DespesaSimples_API.Dtos;
using DespesaSimples_API.Dtos.Balanco;
using DespesaSimples_API.Entities;

namespace DespesaSimples_API.Mappers;

public static class BalancoMapper
{
    public static BalancoDto? MapParaDto(Balanco? balanco)
    {
        if (balanco == null)
            return null;

        return new BalancoDto
        {
            IdBalanco = balanco.IdBalanco,
            Ano = balanco.Ano,
            Mes = balanco.Mes,
            SaldoInicial = balanco.SaldoInicial,
            TotalReceitas = balanco.TotalReceitas,
            TotalDespesas = balanco.TotalDespesas,
            SaldoFinal = balanco.SaldoFinal
        };
    }

    public static Balanco? MapDtoParaEntidade(BalancoDto? balancoDto)
    {
        if (balancoDto == null)
            return null;
        
        return new Balanco
        {
            IdBalanco = balancoDto.IdBalanco,
            Ano = balancoDto.Ano,
            Mes = balancoDto.Mes,
            SaldoInicial = balancoDto.SaldoInicial,
            TotalReceitas = balancoDto.TotalReceitas,
            TotalDespesas = balancoDto.TotalDespesas,
            SaldoFinal = balancoDto.SaldoFinal
        };
    }
}