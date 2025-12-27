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

        // GET: api/Course
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _context.Courses.ToListAsync();
            return Ok(courses);
        }

        // GET: api/Course/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();
            return Ok(course);
        }

        // POST: api/Course
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCourseDto createCourseDto)
        {
            if (createCourseDto == null)
                return BadRequest("Course data is null.");

            if (string.IsNullOrWhiteSpace(createCourseDto.Name))
                return BadRequest("Course name is required.");

            var course = new Course(createCourseDto.Name, createCourseDto.Description);

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
        }

        // PUT: api/Course/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCourseDto updateCourseDto)
        {
            if (updateCourseDto == null)
                return BadRequest("Updated course data is null.");

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return NotFound();

            // Update entity properties using the DTO values
            course.UpdateName(updateCourseDto.Name);
            course.UpdateDescription(updateCourseDto.Description);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Courses.AnyAsync(c => c.Id == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Course/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return NotFound();

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
