namespace Mediporta.Data.UseCases.Interfaces
{
    public interface IGetAllDataUseCase<T> where T : class,IEntity
    {
        Task<IEnumerable<T>> ExecuteAsync();
    }
}