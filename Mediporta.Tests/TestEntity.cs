namespace Mediporta.Tests
{
    public class TestEntity : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TagId { get; set; }
        public int Count { get; set; }
    }
}
