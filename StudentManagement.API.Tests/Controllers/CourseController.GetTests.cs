using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using StudentManagement.API.Tests.Infrastructure;

namespace StudentManagement.API.Tests.Controllers
{
    public class CourseControllerGetTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public CourseControllerGetTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GET_Courses_ReturnsOk()
        {
            // Act
            var response = await _client.GetAsync("/api/course");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GET_Course_ById_ReturnsOk()
        {
            // Arrange — create a course first
            var courseDto = new
            {
                Name = "Physics",
                Credits = 4
            };

            var createResponse = await _client.PostAsJsonAsync("/api/course", courseDto);
            createResponse.EnsureSuccessStatusCode();

            var created = await createResponse.Content.ReadFromJsonAsync<CreatedResponseDto>();
            Assert.NotNull(created);

            // Act
            var response = await _client.GetAsync($"/api/course/{created!.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GET_Course_ById_ReturnsNotFound_ForInvalidId()
        {
            // Act
            var response = await _client.GetAsync($"/api/course/{Guid.NewGuid()}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
