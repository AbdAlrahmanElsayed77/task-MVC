using task.Data;
using task.Models;
using Microsoft.EntityFrameworkCore;

namespace task.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Department> GetAll()
        {
            return _context.Departments
                .Include(d => d.Students)
                .Include(d => d.Instructors)
                .ToList();
        }

        public Department? GetById(int id)
        {
            return _context.Departments
                .Include(d => d.Students)
                .Include(d => d.Instructors)
                .FirstOrDefault(d => d.ID == id);
        }

        public void Add(Department department)
        {
            _context.Departments.Add(department);
        }

        public void Update(Department department)
        {
            _context.Departments.Update(department);
        }

        public void Delete(int id)
        {
            var dept = _context.Departments.Find(id);
            if (dept != null)
                _context.Departments.Remove(dept);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
