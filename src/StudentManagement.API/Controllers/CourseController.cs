using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Infrastructure.Data;
using StudentManagement.Domain.Entities;
using StudentManagement.Application.DTOs.Courses;

namespace StudentManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly StudentManagementDbContext _context;

        public CourseController(StudentManagementDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _context.Courses
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Credits = c.Credits
                })
                .ToListAsync();

            return Ok(courses);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var course = await _context.Courses
                .Where(c => c.Id == id)
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Credits = c.Credits
                })
                .FirstOrDefaultAsync();

            if (course == null) return NotFound();

            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCourseDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Invalid course data.");

            var course = new Course(dto.Name, dto.Description, dto.Credits);

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCourseDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid course data.");

            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();

            course.UpdateName(dto.Name);
            course.UpdateDescription(dto.Description);
            course.UpdateCredits(dto.Credits);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

    // DTO used for GET responses (include Id and Credits)
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Credits { get; set; }
    }
}
