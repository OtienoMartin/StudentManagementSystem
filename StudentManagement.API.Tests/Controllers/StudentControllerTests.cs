using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using StudentManagement.API.Tests.Infrastructure;

public class StudentControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public StudentControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task POST_Student_ReturnsCreated()
    {
        // DTO adjusted to match your API model: FullName and RegistrationNumber
        var studentDto = new
        {
            FullName = "Jane Smith",
            RegistrationNumber = "REG12345"
        };

        var response = await _client.PostAsJsonAsync("/api/student", studentDto);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"POST /api/student failed with status {response.StatusCode}: {errorContent}");
        }

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var created = await response.Content.ReadFromJsonAsync<CreatedResponseDto>();
        Assert.NotNull(created);
        Assert.NotEqual(Guid.Empty, created!.Id);
    }
}

// DTO to deserialize creation response

