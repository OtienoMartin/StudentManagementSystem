using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using StudentManagement.API.Tests.Infrastructure;
using StudentManagement.Application.DTOs.Enrollment;
using StudentManagement.Infrastructure.Data;
using Xunit;

namespace StudentManagement.API.Tests
{
    public class EnrollmentControllerTests
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory _factory;

        public EnrollmentControllerTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();

            using var scope = factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }

        [Fact]
        public async Task POST_Enrollment_ReturnsCreated()
        {
            // Arrange
            var studentId = await CreateStudentAsync();
            var courseId = await CreateCourseAsync();

            var dto = new CreateEnrollmentDto
            {
                StudentId = studentId,
                CourseId = courseId,
                Grade = "A"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/enrollment", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Enrollment POST failed: {error}");
            }

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        private async Task<Guid> CreateStudentAsync()
        {
            var dto = new
            {
                FullName = "John Doe",
                RegistrationNumber = "REG12345"
            };

            var response = await _client.PostAsJsonAsync("/api/student", dto);
            response.EnsureSuccessStatusCode();

            var created =
                await response.Content.ReadFromJsonAsync<CreatedResponseDto>();

            return created!.Id;
        }

        private async Task<Guid> CreateCourseAsync()
        {
            var dto = new
            {
                Name = "Computer Science",
                Credits = 3
            };

            var response = await _client.PostAsJsonAsync("/api/course", dto);
            response.EnsureSuccessStatusCode();

            var created =
                await response.Content.ReadFromJsonAsync<CreatedResponseDto>();

            return created!.Id;
        }
    }
}
