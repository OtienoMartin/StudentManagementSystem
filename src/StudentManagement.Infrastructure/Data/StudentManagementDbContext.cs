using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Infrastructure.Data
{
    public class StudentManagementDbContext : DbContext
    {
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();

        public StudentManagementDbContext(DbContextOptions<StudentManagementDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.FullName).IsRequired();
                entity.Property(s => s.RegistrationNumber).IsRequired();
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired();
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Student)
                      .WithMany(s => s.Enrollments)
                      .HasForeignKey(e => e.StudentId);

                entity.HasOne(e => e.Course)
                      .WithMany()
                      .HasForeignKey(e => e.CourseId);
            });
        }
    }
}
