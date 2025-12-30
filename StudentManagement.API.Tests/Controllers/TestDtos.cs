namespace StudentManagement.API.Tests.Controllers
{
    public class CreatedResponseDto
    {
        public Guid Id { get; set; }
    }

    public class StudentDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = default!;
        public string RegistrationNumber { get; set; } = default!;
    }
}
