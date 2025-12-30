using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using StudentManagement.API.Tests.Infrastructure;

namespace StudentManagement.API.Tests.Controllers
{
    public class StudentControllerPutDeleteTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public StudentControllerPutDeleteTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task PUT_Student_ReturnsNoContent_WhenUpdated()
        {
            // Arrange: create student
            var createResponse = await _client.PostAsJsonAsync("/api/student", new
            {
                FullName = "Initial Name",
                RegistrationNumber = "REG001"
            });
            createResponse.EnsureSuccessStatusCode();
            var created = await createResponse.Content.ReadFromJsonAsync<CreatedResponseDto>();
            var studentId = created!.Id;

            // Act: update student
            var updateDto = new
            {
                FullName = "Updated Name",
                RegistrationNumber = "REG002"
            };

            var putResponse = await _client.PutAsJsonAsync($"/api/student/{studentId}", updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);

            // Verify update
            var getResponse = await _client.GetAsync($"/api/student/{studentId}");
            getResponse.EnsureSuccessStatusCode();
            var updatedStudent = await getResponse.Content.ReadFromJsonAsync<StudentDto>();
            Assert.Equal("Updated Name", updatedStudent!.FullName);
            Assert.Equal("REG002", updatedStudent.RegistrationNumber);
        }

        [Fact]
        public async Task PUT_Student_ReturnsNotFound_WhenStudentDoesNotExist()
        {
            var updateDto = new
            {
                FullName = "Does Not Exist",
                RegistrationNumber = "NOPE"
            };

            var putResponse = await _client.PutAsJsonAsync($"/api/student/{Guid.NewGuid()}", updateDto);

            Assert.Equal(HttpStatusCode.NotFound, putResponse.StatusCode);
        }

        [Fact]
        public async Task DELETE_Student_ReturnsNoContent_WhenDeleted()
        {
            // Arrange: create student
            var createResponse = await _client.PostAsJsonAsync("/api/student", new
            {
                FullName = "Delete Me",
                RegistrationNumber = "DEL001"
            });
            createResponse.EnsureSuccessStatusCode();
            var created = await createResponse.Content.ReadFromJsonAsync<CreatedResponseDto>();
            var studentId = created!.Id;

            // Act: delete student
            var deleteResponse = await _client.DeleteAsync($"/api/student/{studentId}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

            // Verify deleted
            var getResponse = await _client.GetAsync($"/api/student/{studentId}");
            Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
        }

        [Fact]
        public async Task DELETE_Student_ReturnsNotFound_WhenStudentDoesNotExist()
        {
            var deleteResponse = await _client.DeleteAsync($"/api/student/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.NotFound, deleteResponse.StatusCode);
        }
    }
}
