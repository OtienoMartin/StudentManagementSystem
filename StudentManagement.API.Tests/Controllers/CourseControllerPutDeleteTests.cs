using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using StudentManagement.API.Tests.Infrastructure;

namespace StudentManagement.API.Tests.Controllers
{
    public class CourseControllerPutDeleteTests
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public CourseControllerPutDeleteTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        private record CourseResponseDto(
            Guid Id,
            string Name,
            string? Description,
            int Credits
        );

        [Fact]
        public async Task PUT_Course_UpdatesCourse_ReturnsNoContent()
        {
            // Arrange
            var createResponse = await _client.PostAsJsonAsync("/api/course", new
            {
                Name = "Physics",
                Description = "Basic Physics",
                Credits = 4
            });

            createResponse.EnsureSuccessStatusCode();

            var created =
                await createResponse.Content.ReadFromJsonAsync<CourseResponseDto>();

            Assert.NotNull(created);

            var updateDto = new
            {
                Name = "Advanced Physics",
                Description = "Updated",
                Credits = 5
            };

            // Act
            var putResponse =
                await _client.PutAsJsonAsync($"/api/course/{created!.Id}", updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);
        }

        [Fact]
        public async Task DELETE_Course_DeletesCourse_ReturnsNoContent()
        {
            // Arrange
            var createResponse = await _client.PostAsJsonAsync("/api/course", new
            {
                Name = "Chemistry",
                Description = "Intro",
                Credits = 3
            });

            createResponse.EnsureSuccessStatusCode();

            var created =
                await createResponse.Content.ReadFromJsonAsync<CourseResponseDto>();

            Assert.NotNull(created);

            // Act
            var deleteResponse =
                await _client.DeleteAsync($"/api/course/{created!.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

            // Verify deletion
            var getResponse =
                await _client.GetAsync($"/api/course/{created.Id}");

            Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
        }
    }
}
