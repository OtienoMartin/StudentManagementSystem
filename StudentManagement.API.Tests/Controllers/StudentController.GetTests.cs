using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using StudentManagement.API.Tests.Infrastructure;

public class StudentControllerGetTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public StudentControllerGetTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GET_Students_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/student");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GET_StudentById_ReturnsOk_WhenExists()
    {
        // Arrange – create a new student first
        var createResponse = await _client.PostAsJsonAsync("/api/student", new
        {
            FullName = "Test Student",
            RegistrationNumber = "REG999"
        });

        createResponse.EnsureSuccessStatusCode(); // Fail early if creation failed

        var created = await createResponse.Content.ReadFromJsonAsync<CreatedResponseDto>();
        Assert.NotNull(created);
        Assert.NotEqual(Guid.Empty, created.Id);

        Console.WriteLine($"Created student ID: {created.Id}");

        // Act – get the student by the created Id
        var response = await _client.GetAsync($"/api/student/{created.Id}");

        Console.WriteLine($"GET /api/student/{created.Id} returned status: {response.StatusCode}");

        // Assert – expect OK status code
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GET_StudentById_ReturnsNotFound_WhenMissing()
    {
        var response = await _client.GetAsync($"/api/student/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}

// Helper DTO for reading the created response Id
public class CreatedResponseDto
{
    public Guid Id { get; set; }
}
