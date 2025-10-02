using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using task.Data;
using task.Models;
using System.Linq;

namespace task.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;

        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var courses = _context.Courses
                .Include(c => c.Department)
                .ToList();
            return View(courses);
        }

        public IActionResult Details(int id)
        {
            var course = _context.Courses
                .Include(c => c.Department)
                .Include(c => c.Enrollments)
                    .ThenInclude(e => e.Student)
                .Include(c => c.Teaches)
                    .ThenInclude(t => t.Instructor)
                .FirstOrDefault(c => c.Num == id);

            if (course == null) return NotFound();
            return View(course);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Course course)
        {
            if (course.MinDegree >= course.Degree)
                ModelState.AddModelError(nameof(course.MinDegree), "MinDegree must be less than Degree");

            if (_context.Courses.Any(c => c.Name == course.Name))
                ModelState.AddModelError(nameof(course.Name), "Course name must be unique");

            if (ModelState.IsValid)
            {
                _context.Courses.Add(course);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name", course.DepartmentId);
            return View(course);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null) return NotFound();

            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name", course.DepartmentId);
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Course course)
        {
            if (course.MinDegree >= course.Degree)
                ModelState.AddModelError(nameof(course.MinDegree), "MinDegree must be less than Degree");

            if (_context.Courses.Any(c => c.Name == course.Name && c.Num != course.Num))
                ModelState.AddModelError(nameof(course.Name), "Course name must be unique");

            if (ModelState.IsValid)
            {
                _context.Courses.Update(course);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name", course.DepartmentId);
            return View(course);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var course = _context.Courses
                .Include(c => c.Department)
                .FirstOrDefault(c => c.Num == id);
            if (course == null) return NotFound();
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null) return NotFound();
            _context.Courses.Remove(course);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
