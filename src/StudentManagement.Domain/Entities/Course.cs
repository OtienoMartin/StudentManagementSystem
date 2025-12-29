using System;

namespace StudentManagement.Domain.Entities
{
    public class Course
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }
        public int Credits { get; private set; }

        // EF Core
        private Course() { }

        public Course(string name, string? description, int credits)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Course name cannot be empty.");

            if (credits <= 0)
                throw new ArgumentException("Credits must be greater than zero.");

            Name = name;
            Description = description;
            Credits = credits;
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Course name cannot be empty.");

            Name = name;
        }

        public void UpdateDescription(string? description)
        {
            Description = description;
        }

        public void UpdateCredits(int credits)
        {
            if (credits <= 0)
                throw new ArgumentException("Credits must be greater than zero.");

            Credits = credits;
        }
    }
}
