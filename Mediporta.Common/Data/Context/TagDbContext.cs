namespace Mediporta.Common.Data.Context
{
    public class TagDbContext<T> : DbContext where T : class, IEntity
    {
        public TagDbContext(DbContextOptions<TagDbContext<T>> options) : base(options) { }
        public DbSet<T> Data { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=tags.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T>().ToTable(typeof(T).Name.ToString());

            modelBuilder.Entity<T>().HasKey(t => t.TagId);
        }
    }


}
