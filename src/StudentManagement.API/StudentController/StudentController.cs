using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Infrastructure.Data;
using StudentManagement.Domain.Entities;

namespace StudentManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly StudentManagementDbContext _context;

        public StudentController(StudentManagementDbContext context)
        {
            _context = context;
        }

        // GET: api/Student
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _context.Students.ToListAsync();
            return Ok(students);
        }

        // GET: api/Student/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        // POST: api/Student
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Student student)
        {
            if (student == null)
                return BadRequest("Student data is null.");

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        }

        // PUT: api/Student/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Student updatedStudent)
        {
            if (updatedStudent == null)
                return BadRequest("Updated student data is null.");

            if (id != updatedStudent.Id)
                return BadRequest("ID mismatch.");

            _context.Entry(updatedStudent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Students.AnyAsync(s => s.Id == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // DELETE: api/Student/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return NotFound();

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
