using System;
using FluentAssertions;
using StudentManagement.Domain.Entities;
using Xunit;

namespace StudentManagement.Tests.Domain
{
    public class EnrollmentTests
    {
        [Fact]
        public void Constructor_ShouldCreateEnrollment_WhenDataIsValid()
        {
            // Arrange
            var studentId = Guid.NewGuid();
            var courseId = Guid.NewGuid();
            var grade = "A";

            // Act
            var enrollment = new Enrollment(studentId, courseId, grade);

            // Assert
            enrollment.Id.Should().NotBe(Guid.Empty);
            enrollment.StudentId.Should().Be(studentId);
            enrollment.CourseId.Should().Be(courseId);
            enrollment.Grade.Should().Be(grade);
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenStudentIdIsEmpty()
        {
            // Act
            Action act = () =>
                new Enrollment(Guid.Empty, Guid.NewGuid(), "A");

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("*StudentId*");
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenCourseIdIsEmpty()
        {
            // Act
            Action act = () =>
                new Enrollment(Guid.NewGuid(), Guid.Empty, "A");

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("*CourseId*");
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenGradeIsEmpty()
        {
            // Act
            Action act = () =>
                new Enrollment(Guid.NewGuid(), Guid.NewGuid(), "");

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("*Grade*");
        }

        [Fact]
        public void UpdateStudentId_ShouldUpdate_WhenValid()
        {
            // Arrange
            var enrollment = CreateValidEnrollment();
            var newStudentId = Guid.NewGuid();

            // Act
            enrollment.UpdateStudentId(newStudentId);

            // Assert
            enrollment.StudentId.Should().Be(newStudentId);
        }

        [Fact]
        public void UpdateStudentId_ShouldThrow_WhenEmpty()
        {
            var enrollment = CreateValidEnrollment();

            Action act = () =>
                enrollment.UpdateStudentId(Guid.Empty);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void UpdateCourseId_ShouldUpdate_WhenValid()
        {
            var enrollment = CreateValidEnrollment();
            var newCourseId = Guid.NewGuid();

            enrollment.UpdateCourseId(newCourseId);

            enrollment.CourseId.Should().Be(newCourseId);
        }

        [Fact]
        public void UpdateCourseId_ShouldThrow_WhenEmpty()
        {
            var enrollment = CreateValidEnrollment();

            Action act = () =>
                enrollment.UpdateCourseId(Guid.Empty);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void UpdateGrade_ShouldUpdate_WhenValid()
        {
            var enrollment = CreateValidEnrollment();

            enrollment.UpdateGrade("B");

            enrollment.Grade.Should().Be("B");
        }

        [Fact]
        public void UpdateGrade_ShouldThrow_WhenInvalid()
        {
            var enrollment = CreateValidEnrollment();

            Action act = () =>
                enrollment.UpdateGrade(" ");

            act.Should().Throw<ArgumentException>();
        }

        // Helper method
        private static Enrollment CreateValidEnrollment()
        {
            return new Enrollment(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "A"
            );
        }
    }
}
