using task.Data;
using task.Models;

namespace task.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Student> Students { get; }
        IGenericRepository<Department> Departments { get; }
        IGenericRepository<Course> Courses { get; }
        IGenericRepository<Instructor> Instructors { get; }
        IGenericRepository<Enrollment> Enrollments { get; }
        IGenericRepository<Teaches> Teaches { get; }
        int Save();
    }
}
