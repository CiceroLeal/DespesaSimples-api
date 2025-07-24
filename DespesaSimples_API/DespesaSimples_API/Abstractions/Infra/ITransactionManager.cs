namespace DespesaSimples_API.Abstractions.Infra;

public interface ITransactionManager
{
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}