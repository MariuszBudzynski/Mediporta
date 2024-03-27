namespace Mediporta.Data.UseCases.Interfaces
{
    public interface IForceLoadDataUseCase<T> where T : IEntity
    {
        Task ExecuteAsync(IEnumerable<T> data);
    }
}