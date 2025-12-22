using System;

namespace StudentManagement.Domain.ValueObjects
{
    public class Grade
    {
        public int Marks { get; }

        public string Letter =>
            Marks >= 70 ? "A" :
            Marks >= 60 ? "B" :
            Marks >= 50 ? "C" : "F";

        public Grade(int marks)
        {
            if (marks < 0 || marks > 100)
                throw new ArgumentOutOfRangeException(nameof(marks), "Marks must be between 0 and 100");

            Marks = marks;
        }
    }
}
