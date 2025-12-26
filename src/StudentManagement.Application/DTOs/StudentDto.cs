namespace StudentManagement.Application.DTOs.Students
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string RegistrationNumber { get; set; } = null!;
    }
}
