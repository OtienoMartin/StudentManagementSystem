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
            if (studentId == Guid.Empty)
                throw new ArgumentException("StudentId cannot be empty.", nameof(studentId));

            if (courseId == Guid.Empty)
                throw new ArgumentException("CourseId cannot be empty.", nameof(courseId));

            if (string.IsNullOrWhiteSpace(grade))
                throw new ArgumentException("Grade cannot be empty or whitespace.", nameof(grade));

            StudentId = studentId;
            CourseId = courseId;
            Grade = grade;
        }

        public void UpdateStudentId(Guid studentId)
        {
            if (studentId == Guid.Empty)
                throw new ArgumentException("StudentId cannot be empty.", nameof(studentId));

            StudentId = studentId;
        }

        public void UpdateCourseId(Guid courseId)
        {
            if (courseId == Guid.Empty)
                throw new ArgumentException("CourseId cannot be empty.", nameof(courseId));

            CourseId = courseId;
        }

        public void UpdateGrade(string grade)
        {
            if (string.IsNullOrWhiteSpace(grade))
                throw new ArgumentException("Grade cannot be empty or whitespace.", nameof(grade));

            Grade = grade;
        }
    }
}
