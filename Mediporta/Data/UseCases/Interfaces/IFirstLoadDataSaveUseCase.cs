namespace Mediporta.Data.UseCases.Interfaces
{
    public interface IFirstLoadDataSaveUseCase<T> where T : IEntity
    {
        Task ExecuteAsync(IEnumerable<T> posts);
    }
}