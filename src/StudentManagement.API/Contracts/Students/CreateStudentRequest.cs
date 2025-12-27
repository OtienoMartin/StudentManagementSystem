using System.ComponentModel.DataAnnotations;

namespace StudentManagement.API.Contracts.Students
{
    public class CreateStudentRequest
    {
        [Required(ErrorMessage = "Full name is required")]
        [MinLength(3, ErrorMessage = "Full name must be at least 3 characters")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Registration number is required")]
        [MinLength(5, ErrorMessage = "Registration number must be at least 5 characters")]
        public string RegistrationNumber { get; set; } = string.Empty;
    }
}
