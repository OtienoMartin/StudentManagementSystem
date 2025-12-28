using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Enrollment;
using StudentManagement.Domain.Entities;
using StudentManagement.Infrastructure.Data;

namespace StudentManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly StudentManagementDbContext _context;

        public EnrollmentController(StudentManagementDbContext context)
        {
            _context = context;
        }

        // GET: api/enrollment
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToListAsync();

            return Ok(enrollments);
        }

        // GET: api/enrollment/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var enrollment = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (enrollment == null)
                return NotFound();

            return Ok(enrollment);
        }

        // POST: api/enrollment
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEnrollmentDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var enrollment = new Enrollment(
                dto.StudentId,
                dto.CourseId,
                dto.Grade
            );

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = enrollment.Id },
                new { id = enrollment.Id }
            );
        }

        // PUT: api/enrollment/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEnrollmentDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
                return NotFound();

            enrollment.UpdateStudentId(dto.StudentId);
            enrollment.UpdateCourseId(dto.CourseId);
            enrollment.UpdateGrade(dto.Grade);

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/enrollment/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
                return NotFound();

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
