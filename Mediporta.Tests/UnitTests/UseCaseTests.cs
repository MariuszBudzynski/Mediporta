namespace Mediporta.Tests.UnitTests
{
    public class UseCaseTests
    {
        [Fact]
        public async Task ExecuteAsyncShouldReturnAllData()
        {
            // Arrange
            var testData = new List<TestEntity>
            {
                new TestEntity { Id = 1, Name = "Test1" },
                new TestEntity { Id = 2, Name = "Test2" }
            };

            var repositoryMock = new Mock<IDataRepository<TestEntity>>();
            repositoryMock.Setup(repo => repo.GetAllDataAsync()).ReturnsAsync(testData.AsQueryable());

            var useCase = new GetAllDataUseCase<TestEntity>(repositoryMock.Object);

            // Act
            var result = (await useCase.ExecuteAsync()).ToList();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(testData.Count, result.Count);
        }



        [Fact]
        public async Task ExecuteAsyncShouldSaveNewTags()
        {
            // Arrange
            var testData = new List<TestEntity>
            {
                new TestEntity { Id = 1, Name = "Test1" },
                new TestEntity { Id = 2, Name = "Test2" }
            };

            var existingTags = new List<TestEntity>
            {
                new TestEntity { Id = 3, Name = "ExistingTag" }
            };

            var mockRepository = new Mock<IDataRepository<TestEntity>>();
            mockRepository.Setup(repo => repo.GetAllDataAsync()).ReturnsAsync(existingTags.AsQueryable());
            mockRepository.Setup(repo => repo.SaveDataAsync(It.IsAny<IEnumerable<TestEntity>>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var useCase = new FirstLoadDataSaveUseCase<TestEntity>(mockRepository.Object);

            // Act
            await useCase.ExecuteAsync(testData);

            // Assert
            mockRepository.Verify(repo => repo.SaveDataAsync(It.IsAny<IEnumerable<TestEntity>>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsyncShouldSaveNewTagsAfterStartingUP()
        {
            // Arrange
            var testData = new List<TestEntity>
            {
                new TestEntity { Id = 1, Name = "Test1" },
                new TestEntity { Id = 2, Name = "Test2" }
            };

            var existingTags = new List<TestEntity>
            {
                new TestEntity { Id = 3, Name = "ExistingTag" }
            };

            var mockRepository = new Mock<IDataRepository<TestEntity>>();
            mockRepository.Setup(repo => repo.GetAllDataAsync()).ReturnsAsync(existingTags.AsQueryable());
            mockRepository.Setup(repo => repo.SaveDataAsync(It.IsAny<IEnumerable<TestEntity>>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var useCase = new FirstLoadDataSaveUseCase<TestEntity>(mockRepository.Object);

            // Act
            await useCase.ExecuteAsync(testData);

            // Assert
            mockRepository.Verify(repo => repo.SaveDataAsync(It.IsAny<IEnumerable<TestEntity>>()), Times.Once);
        }
    }
}
