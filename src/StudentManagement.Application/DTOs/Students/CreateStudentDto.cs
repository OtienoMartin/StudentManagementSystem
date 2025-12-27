using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Application.DTOs
{
    public class CreateStudentDto
    {
        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100, ErrorMessage = "Full Name can't be longer than 100 characters.")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Registration Number is required.")]
        [StringLength(50, ErrorMessage = "Registration Number can't be longer than 50 characters.")]
        public string RegistrationNumber { get; set; } = null!;
    }
}
