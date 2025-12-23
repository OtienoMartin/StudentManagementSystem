using StudentManagement.Application.DTOs;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public void AddStudent(CreateStudentDto dto)
        {
            if (_repository.GetByRegistrationNumber(dto.RegistrationNumber) != null)
                throw new InvalidOperationException("Student already exists");

            var student = new Student(dto.FullName, dto.RegistrationNumber);
            _repository.Add(student);
        }

        public void DeleteStudent(string registrationNumber)
        {
            var student = _repository.GetByRegistrationNumber(registrationNumber);

            if (student == null)
                throw new InvalidOperationException("Student not found");

            _repository.Remove(student);
        }

        public IReadOnlyList<Student> GetStudents()
        {
            return _repository.GetAll();
        }
    }
}
