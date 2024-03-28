namespace Mediporta.Tests.IntegrationTests
{
    public class ApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ApiIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CheckIFTagsCanBeLoaded()
        {
            //Arrange
            var client = _factory.CreateClient();
            //Act
            var result = await client.GetAsync("/tags");
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CheckIFLoadingTagsRequestWorks()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/tags/force-reload");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CheckIfServerIsResponding()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/tags/force-reload");

            // Assert
            Assert.NotNull(response);
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task CheckIFTagListIsEMpty()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/tags");
            var content = await response.Content.ReadAsStringAsync();
            var tags = JsonSerializer.Deserialize<IEnumerable<IEntity>>(content);

            // Assert
            Assert.NotNull(tags);
            Assert.NotEmpty(tags);
        }

    }
}
