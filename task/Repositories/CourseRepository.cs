using Microsoft.EntityFrameworkCore;
using task.Data;
using task.Models;

namespace task.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;

        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Course> GetAll()
        {
            return _context.Courses.ToList();
        }

        public Course? GetById(int id)
        {
            return _context.Courses
                .Include(c => c.Enrollments)
                    .ThenInclude(e => e.Student)
                .Include(c => c.Teaches)
                    .ThenInclude(t => t.Instructor)
                .FirstOrDefault(c => c.Num == id);
        }

        public void Add(Course course)
        {
            _context.Courses.Add(course);
        }

        public void Update(Course course)
        {
            _context.Courses.Update(course);
        }

        public void Delete(int id)
        {
            var course = _context.Courses.Find(id);
            if (course != null)
                _context.Courses.Remove(course);
        }

        public bool ExistsByName(string name, int? excludeId = null)
        {
            return _context.Courses.Any(c => c.Name == name && (!excludeId.HasValue || c.Num != excludeId));
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
