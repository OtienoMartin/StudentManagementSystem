using System;
using FluentAssertions;
using StudentManagement.Domain.Entities;
using Xunit;

namespace StudentManagement.Tests.Domain
{
    public class StudentTests
    {
        [Fact]
        public void Constructor_ShouldCreateStudent_WhenDataIsValid()
        {
            // Arrange
            var fullName = "John Doe";
            var regNumber = "REG123";

            // Act
            var student = new Student(fullName, regNumber);

            // Assert
            student.Id.Should().NotBe(Guid.Empty);
            student.FullName.Should().Be(fullName);
            student.RegistrationNumber.Should().Be(regNumber);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenFullNameIsEmpty()
        {
            // Act
            Action act = () => new Student("", "REG123");

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("*FullName*");
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenRegistrationNumberIsEmpty()
        {
            // Act
            Action act = () => new Student("John Doe", "");

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("*RegistrationNumber*");
        }

        [Fact]
        public void UpdateFullName_ShouldUpdate_WhenValid()
        {
            var student = new Student("John Doe", "REG123");

            student.UpdateFullName("Jane Doe");

            student.FullName.Should().Be("Jane Doe");
        }

        [Fact]
        public void UpdateFullName_ShouldThrow_WhenInvalid()
        {
            var student = new Student("John Doe", "REG123");

            Action act = () => student.UpdateFullName("");

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void UpdateRegistrationNumber_ShouldUpdate_WhenValid()
        {
            var student = new Student("John Doe", "REG123");

            student.UpdateRegistrationNumber("REG999");

            student.RegistrationNumber.Should().Be("REG999");
        }
    }
}
