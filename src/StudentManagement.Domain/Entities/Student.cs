using System;
using System.Collections.Generic;

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
            UpdateFullName(fullName);
            UpdateRegistrationNumber(registrationNumber);
        }

        public void UpdateFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Full Name cannot be empty.", nameof(fullName));
            FullName = fullName;
        }

        public void UpdateRegistrationNumber(string registrationNumber)
        {
            if (string.IsNullOrWhiteSpace(registrationNumber))
                throw new ArgumentException("Registration Number cannot be empty.", nameof(registrationNumber));
            RegistrationNumber = registrationNumber;
        }
    }
}
