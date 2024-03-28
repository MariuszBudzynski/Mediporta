namespace Mediporta.Common.Data.Repository.Entity.Interfaces
{
    public interface IEntity
    {
        int TagId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }
}