namespace Mediporta.Tests.UnitTests
{
    public class ResponseHandlerServiceTests
    {
        [Fact]
        public async Task ReturnResponseShouldThrowExceptionWhenErrorOccurs()
        {
            // Arrange
            var mockGetAllDataUseCase = new Mock<IGetAllDataUseCase<Tag>>();
            var mockSortingService = new Mock<SortingService<ITagDTO>>();
            var mockPaginationService = new Mock<PaginationService<ITagDTO>>();
            var mockStatisticsCalculator = new Mock<StatisticsCalculator<Tag>>();
            var mockAutoDataLoader = new Mock<AutoDataLoader<Tag>>();

            var responseHandlerService = new ResponseHandlerService(
                mockGetAllDataUseCase.Object,
                mockSortingService.Object,
                mockPaginationService.Object,
                mockStatisticsCalculator.Object,
                mockAutoDataLoader.Object
            );

            mockGetAllDataUseCase.Setup(repo => repo.ExecuteAsync()).ThrowsAsync(new Exception("Test Exception"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => responseHandlerService.ReturnResponse(1, 10, "Name", "asc"));
        }
    }
}
