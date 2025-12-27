namespace StudentManagement.Application.DTOs.Enrollment
{
    public class CreateEnrollmentDto
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public string Grade { get; set; } = null!;
    }
}
