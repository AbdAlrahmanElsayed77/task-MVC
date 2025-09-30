using Microsoft.EntityFrameworkCore;
using task.Models;

namespace task.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Teaches> Teaches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Enrollment>()
                .HasKey(e => new { e.StudentId, e.CourseId });

            modelBuilder.Entity<Teaches>()
        .HasKey(t => new { t.InstructorId, t.CourseId });

            modelBuilder.Entity<Student>()
       .HasOne(s => s.Department)
       .WithMany(d => d.Students)
       .HasForeignKey(s => s.DepartmentId)
       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Instructor>()
        .HasOne(i => i.Department)
        .WithMany(d => d.Instructors)
        .HasForeignKey(i => i.DepartmentId)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
        .HasOne(c => c.Department)
        .WithMany(d => d.Courses)
        .HasForeignKey(c => c.DepartmentId)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);

            modelBuilder.Entity<Teaches>()
        .HasOne(t => t.Instructor)
        .WithMany(i => i.Teaches)
        .HasForeignKey(t => t.InstructorId)
        .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Teaches>()
        .HasOne(t => t.Course)
        .WithMany(c => c.Teaches)
        .HasForeignKey(t => t.CourseId)
        .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
