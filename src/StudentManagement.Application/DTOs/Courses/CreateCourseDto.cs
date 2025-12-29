namespace StudentManagement.Application.DTOs.Courses
{
    public class CreateCourseDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Credits { get; set; }
    }
}
