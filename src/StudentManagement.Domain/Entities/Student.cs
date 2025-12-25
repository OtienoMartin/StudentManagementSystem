namespace StudentManagement.Domain.Entities
{
    public class Student
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public string FullName { get; private set; } = string.Empty;
        public string RegistrationNumber { get; private set; } = string.Empty;

        public List<Enrollment> Enrollments { get; private set; } = new();

        private Student() { } // EF Core

        public Student(string fullName, string registrationNumber)
        {
            FullName = fullName;
            RegistrationNumber = registrationNumber;
        }
    }
}
