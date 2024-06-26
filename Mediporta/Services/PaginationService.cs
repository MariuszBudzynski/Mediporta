﻿namespace Mediporta.Services
{
    public class PaginationService<T> where T : ITagDTO
    {
        public IEnumerable<T> PaginateTags(IEnumerable<T> data, int page, int pageSize)
        {
            var startIndex = (page - 1) * pageSize;
            return data.Skip(startIndex).Take(pageSize);
        }
    }
}
