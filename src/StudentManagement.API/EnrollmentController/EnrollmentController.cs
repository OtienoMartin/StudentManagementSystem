using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Infrastructure.Data;
using StudentManagement.Domain.Entities;

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

        // GET: api/Enrollment
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToListAsync();

            return Ok(enrollments);
        }

        // GET: api/Enrollment/{id}
        [HttpGet("{id}")]
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

        // POST: api/Enrollment
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Enrollment enrollment)
        {
            if (enrollment == null)
                return BadRequest("Enrollment data is null.");

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = enrollment.Id }, enrollment);
        }

        // PUT: api/Enrollment/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Enrollment updatedEnrollment)
        {
            if (updatedEnrollment == null)
                return BadRequest("Updated enrollment data is null.");

            if (id != updatedEnrollment.Id)
                return BadRequest("ID mismatch.");

            _context.Entry(updatedEnrollment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Enrollments.AnyAsync(e => e.Id == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // DELETE: api/Enrollment/{id}
        [HttpDelete("{id}")]
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
