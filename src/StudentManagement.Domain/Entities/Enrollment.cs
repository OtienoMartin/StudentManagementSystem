namespace StudentManagement.Domain.Entities
{
    public class Enrollment
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public Guid StudentId { get; private set; }
        public Student Student { get; private set; } = null!;

        public Guid CourseId { get; private set; }
        public Course Course { get; private set; } = null!;

        public string Grade { get; private set; } = string.Empty;

        private Enrollment() { } // Required by EF Core

        public Enrollment(Guid studentId, Guid courseId, string grade)
        {
            StudentId = studentId;
            CourseId = courseId;
            Grade = grade;
        }
    }
}
