namespace Mediporta.Services
{
    public class SortingService<T> where T : class,IEntity
    {
        public IEnumerable<T> SortTags(IEnumerable<T> data, string sortBy, string sortOrder)
        {
            switch (sortBy.ToLower())
            {
                case "name":
                    return sortOrder.ToLower() == "asc" ? data.OrderBy(t => t.Name) : data.OrderByDescending(t => t.Name);

                default:
                    throw new ArgumentException("Invalid sort criteria");
            }
        }
    }
}
