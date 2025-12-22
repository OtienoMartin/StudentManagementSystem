using System;

namespace StudentManagement.Domain.Entities
{
    public class Course
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public required string Name { get; private set; }
        
        public int Credits { get; private set; }

        private Course() { }

        public Course(string name, int credits)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Course name cannot be empty");

            if (credits <= 0)
                throw new ArgumentException("Credits must be positive");

            Name = name;
            Credits = credits;
        }
    }
}
