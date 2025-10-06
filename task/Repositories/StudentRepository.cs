using Microsoft.EntityFrameworkCore;
using task.Data;
using task.Models;

namespace task.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Students.Include(s => s.Department).ToList();
        }

        public Student? GetById(int id)
        {
            return _context.Students
                .Include(s => s.Department)
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                .FirstOrDefault(s => s.Ssn == id);
        }

        public void Add(Student student)
        {
            _context.Students.Add(student);
        }

        public void Update(Student student)
        {
            _context.Students.Update(student);
        }

        public void Delete(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.Ssn == id);
            if (student != null)
                _context.Students.Remove(student);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
