namespace Mediporta.Tests.UnitTests
{
    public class DataRepositoryTests
    {
        [Fact]
        public async Task SaveDataAsyncShouldSaveDataInDatabase()
        {
            // Arrange
            var databaseName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<TagDbContext<TestEntity>>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;
            var dbContext = new TagDbContext<TestEntity>(options);
            var repository = new DataRepository<TestEntity>(dbContext);
            var testData = new List<TestEntity> { new TestEntity { Id = 1, Name = "Test" } };

            // Act
            await repository.SaveDataAsync(testData);

            // Assert
            Assert.Equal(1, await dbContext.Data.CountAsync());
        }

        [Fact]
        public async Task GetAllDataAsyncShouldReturnAllDataFromDatabase()
        {
            // Arrange
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

            // Act
            var result = await repository.GetAllDataAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task UpdateDataAsyncShouldUpdateDataInDatabase()
        {
            // Arrange
            var databaseName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<TagDbContext<TestEntity>>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;
            var dbContext = new TagDbContext<TestEntity>(options);
            var repository = new DataRepository<TestEntity>(dbContext);

            var testData = new List<TestEntity> { new TestEntity { Id = 1, Name = "Test1" } };
            await dbContext.AddRangeAsync(testData);
            await dbContext.SaveChangesAsync();

            // Act
            testData.First().Name = "UpdatedTest";
            await repository.UpdateDataAsync(testData);

            // Assert
            Assert.Equal("UpdatedTest", (await dbContext.Data.FirstAsync()).Name);
        }
    }
}
