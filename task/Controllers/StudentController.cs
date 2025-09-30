using Microsoft.AspNetCore.Mvc;
using task.Data;
using task.Models;

namespace task.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult GetAll()
        {
            var students = _context.Students.ToList();
            return View(students);
        }

        public IActionResult GetOne(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.Ssn == id);
            if (student == null) return NotFound();
            return View(student);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Student student)
        {
          
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction("GetAll");
            
          //  return View(student);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.Ssn == id);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            var existingStudent = _context.Students.FirstOrDefault(s => s.Ssn == student.Ssn);
            if (existingStudent == null) return NotFound();
            existingStudent.Name = student.Name;
            existingStudent.Age = student.Age;
            _context.SaveChanges();
            return RedirectToAction("GetAll");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.Ssn == id);
            if (student == null) return NotFound();
            return View(student); 
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.Ssn == id);
            if (student == null) return NotFound();

            _context.Students.Remove(student);
            _context.SaveChanges();
            return RedirectToAction("GetAll");
        }
    }
}
    