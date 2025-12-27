namespace StudentManagement.API.Contracts.Students;

public class UpdateStudentRequest
{
    public string FullName { get; set; } = string.Empty;
    public string RegistrationNumber { get; set; } = string.Empty;
}
