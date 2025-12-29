using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using StudentManagement.API.Tests.Infrastructure;

namespace StudentManagement.API.Tests.Controllers
{
    public class EnrollmentControllerPutDeleteTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public EnrollmentControllerPutDeleteTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task PUT_Enrollment_ReturnsNoContent_WhenUpdated()
        {
            // Arrange: create student and course
            var studentId = await CreateStudentAsync();
            var courseId = await CreateCourseAsync();

            // Create enrollment
            var createEnrollmentResponse = await _client.PostAsJsonAsync("/api/enrollment", new
            {
                StudentId = studentId,
                CourseId = courseId,
                Grade = "B"
            });
            createEnrollmentResponse.EnsureSuccessStatusCode();
            var created = await createEnrollmentResponse.Content.ReadFromJsonAsync<CreatedResponseDto>();
            var enrollmentId = created!.Id;

            // Prepare update DTO (change grade)
            var updateDto = new
            {
                StudentId = studentId,
                CourseId = courseId,
                Grade = "A"
            };

            // Act: update enrollment
            var putResponse = await _client.PutAsJsonAsync($"/api/enrollment/{enrollmentId}", updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);

            // Verify update by GET
            var getResponse = await _client.GetAsync($"/api/enrollment/{enrollmentId}");
            getResponse.EnsureSuccessStatusCode();

            var updatedEnrollment = await getResponse.Content.ReadFromJsonAsync<EnrollmentDto>();
            Assert.Equal("A", updatedEnrollment!.Grade);
            Assert.Equal(studentId, updatedEnrollment.StudentId);
            Assert.Equal(courseId, updatedEnrollment.CourseId);
        }

        [Fact]
        public async Task PUT_Enrollment_ReturnsNotFound_WhenEnrollmentDoesNotExist()
        {
            var updateDto = new
            {
                StudentId = Guid.NewGuid(),
                CourseId = Guid.NewGuid(),
                Grade = "A"
            };

            var putResponse = await _client.PutAsJsonAsync($"/api/enrollment/{Guid.NewGuid()}", updateDto);

            Assert.Equal(HttpStatusCode.NotFound, putResponse.StatusCode);
        }

        [Fact]
        public async Task DELETE_Enrollment_ReturnsNoContent_WhenDeleted()
        {
            // Arrange: create student and course
            var studentId = await CreateStudentAsync();
            var courseId = await CreateCourseAsync();

            // Create enrollment
            var createEnrollmentResponse = await _client.PostAsJsonAsync("/api/enrollment", new
            {
                StudentId = studentId,
                CourseId = courseId,
                Grade = "B"
            });
            createEnrollmentResponse.EnsureSuccessStatusCode();
            var created = await createEnrollmentResponse.Content.ReadFromJsonAsync<CreatedResponseDto>();
            var enrollmentId = created!.Id;

            // Act: delete enrollment
            var deleteResponse = await _client.DeleteAsync($"/api/enrollment/{enrollmentId}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

            // Verify deletion
            var getResponse = await _client.GetAsync($"/api/enrollment/{enrollmentId}");
            Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
        }

        [Fact]
        public async Task DELETE_Enrollment_ReturnsNotFound_WhenEnrollmentDoesNotExist()
        {
            var deleteResponse = await _client.DeleteAsync($"/api/enrollment/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.NotFound, deleteResponse.StatusCode);
        }

        // Helper to create student for tests
        private async Task<Guid> CreateStudentAsync()
        {
            var studentDto = new
            {
                FullName = "Test Student",
                RegistrationNumber = $"REG-{Guid.NewGuid()}"
            };

            var response = await _client.PostAsJsonAsync("/api/student", studentDto);
            response.EnsureSuccessStatusCode();

            var created = await response.Content.ReadFromJsonAsync<CreatedResponseDto>();
            return created!.Id;
        }

        // Helper to create course for tests
        private async Task<Guid> CreateCourseAsync()
        {
            var courseDto = new
            {
                Name = "Test Course",
                Credits = 3
            };

            var response = await _client.PostAsJsonAsync("/api/course", courseDto);
            response.EnsureSuccessStatusCode();

            var created = await response.Content.ReadFromJsonAsync<CreatedResponseDto>();
            return created!.Id;
        }
    }

    // DTO for deserialization in GET
    public class EnrollmentDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public string Grade { get; set; } = default!;
    }
}
