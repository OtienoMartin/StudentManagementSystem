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

        createResponse.EnsureSuccessStatusCode();

        var created = await createResponse.Content.ReadFromJsonAsync<CreatedResponseDto>();
        Assert.NotNull(created);
        Assert.NotEqual(Guid.Empty, created.Id);

        // Act – get the student by the created Id
        var response = await _client.GetAsync($"/api/student/{created.Id}");

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
