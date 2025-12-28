public class StudentControllerTests 
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public StudentControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    // tests...
}
