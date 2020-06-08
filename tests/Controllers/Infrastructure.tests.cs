using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ProjectName.Api.Tests
{
    public class BasicTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public BasicTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _factory.ClientOptions.HandleCookies = false;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/health")]
        [InlineData("/version")]
        [InlineData("/docs")]
        public async Task Get_Should_Return_Success_Status_Code(string url)
        {
            // Given
            var client = _factory.CreateClient();
            // When
            var response = await client.GetAsync(url);
            // Then
            response.EnsureSuccessStatusCode();
        }
    }
}
