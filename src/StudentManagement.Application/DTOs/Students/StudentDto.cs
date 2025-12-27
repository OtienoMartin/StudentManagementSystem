using System;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Application.DTOs.Students
{
    public class StudentDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters.")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Registration Number is required.")]
        [StringLength(50, ErrorMessage = "Registration Number cannot exceed 50 characters.")]
        public string RegistrationNumber { get; set; } = null!;
    }
}
