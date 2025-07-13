using DespesaSimples_API.Abstractions.Repositories;
using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Dtos;
using DespesaSimples_API.Enums;
using DespesaSimples_API.Mappers;
using DespesaSimples_API.DbContexts;
using DespesaSimples_API.Dtos.Responses;
using DespesaSimples_API.Exceptions;

namespace DespesaSimples_API.Services;

public class BalancoService(IBalancoRepository balancoRepository, ITransacaoRepository transacaoRepository)
    : IBalancoService
{
    public async Task<BalancoResponseDto> ObterPorAnoMesAsync(int ano, int mes)
    {
        var dto = BalancoMapper
            .MapParaDto(await balancoRepository.ObterPorAnoMesAsync(ano, mes) ?? null);
        
        return new BalancoResponseDto
        {
            Balancos = dto != null ? [dto] : []
        };
    } 
        

    public async Task<BalancoResponseDto> ObterPorAnoAsync(int ano)
    {
        var balancos = await balancoRepository.ObterPorAnoAsync(ano);

        if (balancos.Count == 0)
            return new BalancoResponseDto();

        var dtos = balancos
            .Select(BalancoMapper.MapParaDto)
            .Where(b => b != null)
            .OfType<BalancoDto>()
            .ToList();
        
        return new BalancoResponseDto
        {
            Balancos = dtos
        };
    }

    private async Task<BalancoDto> GerarBalancoAsync(int ano, int mes)
    {
        var anoAnterior = mes == 1 ? ano - 1 : ano;
        var mesAnterior = mes == 1 ? 12 : mes - 1;

        var prev = await balancoRepository.ObterPorAnoMesAsync(anoAnterior, mesAnterior);

        // SaldoInicial = saldo final do mês anterior (ou zero se não existir)
        var saldoInicial = prev?.SaldoFinal ?? 0m;

        var receitas = await transacaoRepository.SomarPorTipoAsync(TipoTransacaoEnum.Receita, ano, mes);
        var despesas = await transacaoRepository.SomarPorTipoAsync(TipoTransacaoEnum.Despesa, ano, mes);
        
        return new BalancoDto(ano, mes, saldoInicial, receitas, despesas);
    }

    public async Task<bool> AtualizarBalancoAsync(BalancoDto balancoDto)
    {
        var balanco = BalancoMapper.MapDtoParaEntidade(balancoDto);
        
        if (balanco == null)
            throw new ArgumentException("O balanço não pode ser nulo");

        var exist = await balancoRepository.ObterPorAnoMesAsync(balanco.Ano, balanco.Mes);

        if (exist == null)
            return await balancoRepository.CriarBalancoAsync(balanco);

        exist.SaldoInicial = balanco.SaldoInicial;
        exist.TotalReceitas = balanco.TotalReceitas;
        exist.TotalDespesas = balanco.TotalDespesas;
        exist.SaldoFinal = balanco.SaldoFinal;

        return await balancoRepository.AtualizarBalancosync(exist);
    }

    public async Task<bool> ReprocessarBalancosAPartirDeAsync(DateTime dataAlterada)
    {
        var mesAtualRef = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        var ultimoMesProcessavel = mesAtualRef.AddMonths(-1);

        if (dataAlterada >= mesAtualRef)
            return true;

        var inicio = new DateTime(dataAlterada.Year, dataAlterada.Month, 1);
        var anyChanges = false;

        for (var cursor = inicio; cursor <= ultimoMesProcessavel; cursor = cursor.AddMonths(1))
        {
            var balanco = await GerarBalancoAsync(cursor.Year, cursor.Month);
            var updated = await AtualizarBalancoAsync(balanco);
            anyChanges |= updated;
        }

        return anyChanges;
    }
}