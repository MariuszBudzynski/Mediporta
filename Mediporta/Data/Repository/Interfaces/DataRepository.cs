﻿namespace Mediporta.Data.Repository.Interfaces
{
    public class DataRepository<T> : IDataRepository<T> where T : class, IEntity
    {
        private readonly TagDbContext<T> _context;

        public DataRepository(TagDbContext<T> context)
        {
            _context = context;
        }

        public async Task SaveDataAsync(IEnumerable<T> data)
        {
            await _context.AddRangeAsync(data);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllDataAsync()
        {
            return await _context.Data.ToListAsync();
        }
    }
}
