using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using StudentManagement.API.Tests.Infrastructure;

namespace StudentManagement.API.Tests.Controllers
{
    public class CourseControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public CourseControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task POST_Course_ReturnsCreated()
        {
            // Arrange
            var courseDto = new
            {
                Name = "Computer Science",
                Credits = 3
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/course", courseDto);

            // Debug on failure
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(
                    $"POST /api/course failed with {response.StatusCode}: {error}");
            }

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var created = await response.Content.ReadFromJsonAsync<CreatedResponseDto>();
            Assert.NotNull(created);
            Assert.NotEqual(Guid.Empty, created!.Id);
        }

        // Optional: basic GET test to verify the GET endpoint works
        [Fact]
        public async Task GET_Courses_ReturnsOk()
        {
            var response = await _client.GetAsync("/api/course");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
