using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using task.Models;
using task.Repositories;

namespace task.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IDepartmentRepository _deptRepo;

        public StudentController(IStudentRepository studentRepo, IDepartmentRepository deptRepo)
        {
            _studentRepo = studentRepo;
            _deptRepo = deptRepo;
        }

        public IActionResult GetAll()
        {
            var students = _studentRepo.GetAll();
            return View(students);
        }

        public IActionResult GetOne(int id)
        {
            var student = _studentRepo.GetById(id);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Departments = new SelectList(_deptRepo.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Student student)
        {
            if (ModelState.IsValid)
            {
                _studentRepo.Add(student);
                _studentRepo.Save();
                return RedirectToAction(nameof(GetAll));
            }

            ViewBag.Departments = new SelectList(_deptRepo.GetAll(), "Id", "Name");
            return View(student);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = _studentRepo.GetById(id);
            if (student == null) return NotFound();

            ViewBag.Departments = new SelectList(_deptRepo.GetAll(), "Id", "Name", student.DepartmentId);
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                _studentRepo.Update(student);
                _studentRepo.Save();
                return RedirectToAction(nameof(GetAll));
            }

            ViewBag.Departments = new SelectList(_deptRepo.GetAll(), "Id", "Name", student.DepartmentId);
            return View(student);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var student = _studentRepo.GetById(id);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _studentRepo.Delete(id);
            _studentRepo.Save();
            return RedirectToAction(nameof(GetAll));
        }
    }
}
