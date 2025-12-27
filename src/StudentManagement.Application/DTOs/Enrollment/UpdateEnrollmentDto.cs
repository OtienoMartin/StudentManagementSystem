public class UpdateEnrollmentDto
{
    public Guid StudentId { get; set; }
    public Guid CourseId { get; set; }
    public string Grade { get; set; } = null!;
}
