using System;
using System.Collections.Generic;

namespace StudentManagement.Domain.Entities
{
    public class Student
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string FullName { get; private set; }
        public string RegistrationNumber { get; private set; }

        private readonly List<Enrollment> _enrollments = new();
        public IReadOnlyCollection<Enrollment> Enrollments => _enrollments.AsReadOnly();

        private Student() { } // For ORM or serialization

        public Student(string fullName, string registrationNumber)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Student name cannot be empty");

            if (string.IsNullOrWhiteSpace(registrationNumber))
                throw new ArgumentException("Registration number cannot be empty");

            FullName = fullName;
            RegistrationNumber = registrationNumber;
        }

        public void Enroll(Enrollment enrollment)
        {
            _enrollments.Add(enrollment);
        }
    }
}
