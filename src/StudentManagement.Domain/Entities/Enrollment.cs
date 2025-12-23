using StudentManagement.Domain.ValueObjects;
using System;

namespace StudentManagement.Domain.Entities
{
    public class Enrollment
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Course Course { get; private set; } = null!;
        public Grade Grade { get; private set; } = null!;

        private Enrollment() { }

        public Enrollment(Course course, Grade grade)
        {
            Course = course ?? throw new ArgumentNullException(nameof(course));
            Grade = grade ?? throw new ArgumentNullException(nameof(grade));
        }
    }
}
