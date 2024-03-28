namespace Mediporta.Common.Data.Repository.Interfaces
{
    public interface IDataRepository<T> where T : class, IEntity
    {
        Task<IEnumerable<T>> GetAllDataAsync();
        Task SaveDataAsync(IEnumerable<T> data);
        Task UpdateDataAsync(IEnumerable<T> data);
    }
}