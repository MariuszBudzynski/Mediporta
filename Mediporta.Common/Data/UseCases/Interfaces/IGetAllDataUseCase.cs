namespace Mediporta.Common.Data.UseCases.Interfaces
{
    public interface IGetAllDataUseCase<T> where T : IEntity
    {
        Task<IEnumerable<T>> ExecuteAsync();
    }
}