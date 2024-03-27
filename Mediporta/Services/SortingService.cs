namespace Mediporta.Services
{
    public class SortingService<T> where T : ITagDTO
    {
        public IEnumerable<T> SortTags(IEnumerable<T> data, string sortBy, string sortOrder)
        {
            switch (sortBy.ToLower())
            {
                case "name":
                    return sortOrder.ToLower() == "asc" ? data.OrderBy(t => t.Name) : data.OrderByDescending(t => t.Name);

                case "count":
                    return sortOrder.ToLower() == "asc" ? data.OrderBy(t => t.Count) : data.OrderByDescending(t => t.Count);

                default:
                    throw new ArgumentException("Invalid sort criteria");
            }
        }
    }
}
