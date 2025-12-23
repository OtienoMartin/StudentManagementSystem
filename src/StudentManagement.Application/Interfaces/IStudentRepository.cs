using StudentManagement.Domain.Entities;
using System;
using System.Collections.Generic;

namespace StudentManagement.Application.Interfaces
{
    public interface IStudentRepository
    {
        void Add(Student student);
        void Remove(Student student);
        Student? GetById(Guid id);
        Student? GetByRegistrationNumber(string registrationNumber);
        IReadOnlyList<Student> GetAll();
    }
}
