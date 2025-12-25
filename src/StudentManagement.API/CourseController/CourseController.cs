using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Infrastructure.Data;
using StudentManagement.Domain.Entities;

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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();
            return Ok(course);
        }

        // POST: api/Course
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Course course)
        {
            if (course == null)
                return BadRequest("Course data is null.");

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            // Returns 201 Created with route to GET this newly created course
            return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
        }

        // PUT: api/Course/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Course updatedCourse)
        {
            if (updatedCourse == null)
                return BadRequest("Updated course data is null.");

            if (id != updatedCourse.Id)
                return BadRequest("ID mismatch.");

            _context.Entry(updatedCourse).State = EntityState.Modified;

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
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
