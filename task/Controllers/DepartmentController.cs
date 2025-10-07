using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using task.Filters;
using task.Models;
using task.Repositories;
using task.ViewModels;

namespace task.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _deptRepo;

        public DepartmentController(IDepartmentRepository deptRepo)
        {
            _deptRepo = deptRepo;
        }

        public IActionResult GetAll()
        {
            var depts = _deptRepo.GetAll()
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
                _deptRepo.Add(department);
                _deptRepo.Save();
                return RedirectToAction(nameof(GetAll));
            }
            return View(department);
        }
    }
}
