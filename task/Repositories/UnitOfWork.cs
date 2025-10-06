using task.Data;
using task.Models;

namespace task.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IGenericRepository<Student> Students { get; }
        public IGenericRepository<Department> Departments { get; }
        public IGenericRepository<Course> Courses { get; }
        public IGenericRepository<Instructor> Instructors { get; }
        public IGenericRepository<Enrollment> Enrollments { get; }
        public IGenericRepository<Teaches> Teaches { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Students = new GenericRepository<Student>(_context);
            Departments = new GenericRepository<Department>(_context);
            Courses = new GenericRepository<Course>(_context);
            Instructors = new GenericRepository<Instructor>(_context);
            Enrollments = new GenericRepository<Enrollment>(_context);
            Teaches = new GenericRepository<Teaches>(_context);
        }

        public int Save() => _context.SaveChanges();

        public void Dispose() => _context.Dispose();
    }
}
