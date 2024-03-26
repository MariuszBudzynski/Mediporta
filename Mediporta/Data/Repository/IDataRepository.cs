
namespace Mediporta.Data.Repository
{
    public interface IDataRepository<T> where T : class, IEntity
    {
        Task<IEnumerable<T>> GetAllDataAsync();
        Task SaveDataAsync(IEnumerable<T> data);
    }
}