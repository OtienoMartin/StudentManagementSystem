namespace StudentManagement.Domain.Entities
{
    public class Student
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public string FullName { get; private set; } = null!;
        public string RegistrationNumber { get; private set; } = null!;

        public List<Enrollment> Enrollments { get; private set; } = new();

        protected Student() { }

        public Student(string fullName, string registrationNumber)
        {
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            RegistrationNumber = registrationNumber ?? throw new ArgumentNullException(nameof(registrationNumber));
        }

        public void UpdateFullName(string fullName)
        {
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
        }

        public void UpdateRegistrationNumber(string registrationNumber)
        {
            RegistrationNumber = registrationNumber ?? throw new ArgumentNullException(nameof(registrationNumber));
        }
    }
}
