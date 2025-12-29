using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using StudentManagement.API.Tests.Infrastructure; // Import for CustomWebApplicationFactory and CreatedResponseDto

namespace StudentManagement.API.Tests.Controllers
{
    public class EnrollmentControllerGetTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public EnrollmentControllerGetTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GET_Enrollments_ReturnsOk()
        {
            var response = await _client.GetAsync("/api/enrollment");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GET_EnrollmentById_ReturnsOk_WhenExists()
        {
            // Arrange: create dependencies
            var studentId = await CreateStudentAsync();
            var courseId = await CreateCourseAsync();

            // Create Enrollment
            var createEnrollmentResponse = await _client.PostAsJsonAsync("/api/enrollment", new
            {
                StudentId = studentId,
                CourseId = courseId,
                Grade = "A"
            });
            createEnrollmentResponse.EnsureSuccessStatusCode();

            var created = await createEnrollmentResponse.Content.ReadFromJsonAsync<CreatedResponseDto>();
            Assert.NotNull(created);

            // Act: get enrollment by id
            var response = await _client.GetAsync($"/api/enrollment/{created!.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GET_EnrollmentById_ReturnsNotFound_WhenMissing()
        {
            var response = await _client.GetAsync($"/api/enrollment/{Guid.NewGuid()}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        // Helpers
        private async Task<Guid> CreateStudentAsync()
        {
            var studentDto = new
            {
                FullName = "Test Student",
                RegistrationNumber = "REG100"
            };

            var response = await _client.PostAsJsonAsync("/api/student", studentDto);
            response.EnsureSuccessStatusCode();

            var created = await response.Content.ReadFromJsonAsync<CreatedResponseDto>();
            return created!.Id;
        }

        private async Task<Guid> CreateCourseAsync()
        {
            var courseDto = new
            {
                Name = "Mathematics",
                Credits = 4
            };

            var response = await _client.PostAsJsonAsync("/api/course", courseDto);
            response.EnsureSuccessStatusCode();

            var created = await response.Content.ReadFromJsonAsync<CreatedResponseDto>();
            return created!.Id;
        }
    }
}
