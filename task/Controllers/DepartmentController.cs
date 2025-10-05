using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using task.Data;
using task.Models;
using task.ViewModels;
using task.Filters;

namespace task.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;

        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult GetAll()
        {
            var depts = _context.Departments
                .Include(d => d.Students)
                .Include(d => d.Instructors)
                .Select(d => new DepartmentVM
                {
                    Name = d.Name,
                    Manager = d.Manger,
                    StdCount = d.Students.Count,
                    InsCount = d.Instructors.Count,
                    StdNames = d.Students.Select(s => s.Name).ToList(),
                    InsNames = d.Instructors.Select(i => i.Name).ToList()
                }).ToList();

            return View(depts);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [DepartmentLocationFilter]
        [AddFooterFilter]
        public IActionResult Add(Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Add(department);
                _context.SaveChanges();
                return RedirectToAction(nameof(GetAll));
            }
            return View(department);
        }

    }
}
