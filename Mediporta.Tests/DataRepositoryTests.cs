namespace Mediporta.Tests
{
    public class DataRepositoryTests
    {
        [Fact]
        public async Task SaveDataAsync_Should_Save_Data_In_Database()
        {
            var databaseName = Guid.NewGuid().ToString();

            var options = new DbContextOptionsBuilder<TagDbContext<TestEntity>>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;
            var dbContext = new TagDbContext<TestEntity>(options);
            var repository = new DataRepository<TestEntity>(dbContext);
            var testData = new List<TestEntity> { new TestEntity { Id = 1, Name = "Test" } };

            await repository.SaveDataAsync(testData);

            Assert.Equal(1, await dbContext.Data.CountAsync());

        }

        [Fact]
        public async Task GetAllDataAsync_Should_Return_All_Data_From_Database()
        {
            var databaseName = Guid.NewGuid().ToString();


            var options = new DbContextOptionsBuilder<TagDbContext<TestEntity>>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;
            var dbContext = new TagDbContext<TestEntity>(options);
            var repository = new DataRepository<TestEntity>(dbContext);
            var testData = new List<TestEntity>
            {
                new TestEntity { Id = 1, Name = "Test1" },
                new TestEntity { Id = 2, Name = "Test2" }
            };
            await dbContext.AddRangeAsync(testData);
            await dbContext.SaveChangesAsync();

            var result = await repository.GetAllDataAsync();

            Assert.Equal(2, result.Count());

        }

        [Fact]
        public async Task UpdateDataAsync_Should_Update_Data_In_Database()
        {
            var databaseName = Guid.NewGuid().ToString();

            var options = new DbContextOptionsBuilder<TagDbContext<TestEntity>>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;
            var dbContext = new TagDbContext<TestEntity>(options);
            var repository = new DataRepository<TestEntity>(dbContext);

            var testData = new List<TestEntity> { new TestEntity { Id = 1, Name = "Test1" } };

            await dbContext.AddRangeAsync(testData);
            await dbContext.SaveChangesAsync();

            testData.First().Name = "UpdatedTest";
            await repository.UpdateDataAsync(testData);

            Assert.Equal("UpdatedTest", (await dbContext.Data.FirstAsync()).Name);

        }
    }

    public class TestEntity : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TagId { get; set; }
        public int Count { get; set; }
    }
}
