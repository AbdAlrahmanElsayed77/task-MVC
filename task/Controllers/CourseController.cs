using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using task.Models;
using task.Repositories;

namespace task.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepo;
        private readonly IDepartmentRepository _deptRepo;

        public CourseController(ICourseRepository courseRepo, IDepartmentRepository deptRepo)
        {
            _courseRepo = courseRepo;
            _deptRepo = deptRepo;
        }

        public IActionResult Index()
        {
            var courses = _courseRepo.GetAll();
            return View(courses);
        }

        public IActionResult Details(int id)
        {
            var course = _courseRepo.GetById(id);
            if (course == null) return NotFound();
            return View(course);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Departments = new SelectList(_deptRepo.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Course course)
        {
            if (course.MinDegree >= course.Degree)
                ModelState.AddModelError(nameof(course.MinDegree), "MinDegree must be less than Degree");

            if (_courseRepo.ExistsByName(course.Name))
                ModelState.AddModelError(nameof(course.Name), "Course name must be unique");

            if (ModelState.IsValid)
            {
                _courseRepo.Add(course);
                _courseRepo.Save();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments = new SelectList(_deptRepo.GetAll(), "Id", "Name");
            return View(course);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = _courseRepo.GetById(id);
            if (course == null) return NotFound();

            ViewBag.Departments = new SelectList(_deptRepo.GetAll(), "Id", "Name");
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Course course)
        {
            if (course.MinDegree >= course.Degree)
                ModelState.AddModelError(nameof(course.MinDegree), "MinDegree must be less than Degree");

            if (_courseRepo.ExistsByName(course.Name, course.Num))
                ModelState.AddModelError(nameof(course.Name), "Course name must be unique");

            if (ModelState.IsValid)
            {
                _courseRepo.Update(course);
                _courseRepo.Save();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments = new SelectList(_deptRepo.GetAll(), "Id", "Name");
            return View(course);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var course = _courseRepo.GetById(id);
            if (course == null) return NotFound();
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _courseRepo.Delete(id);
            _courseRepo.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
