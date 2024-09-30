using Company.Data.Entities;
using Company.Repository.Interfacies;
using Company.Service.DTO;
using Company.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;

namespace Company.web.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService context)
        {
            _departmentService = context;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var dep = _departmentService.GetAll();
            return View("Index" , dep);
        }

        [HttpGet]
        public IActionResult Create() 
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _departmentService.Add(model);
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("DepartmentError", " ValidationErrors");
                return View(model);
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("DepartmentError", ex.Message);
                return View(model);
            }
        }

        
        public IActionResult Details(int? id) 
        {
            var dep = _departmentService.GetbyId(id);
            
            if (dep is null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }
            return View("Details", dep);
        }

        [HttpPost]
        public IActionResult Update(int? id , DepartmentDTO model)
        {
            var dep = _departmentService.GetbyId(model.Id);
            if (dep is null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }

            dep.code = model.code;
            dep.Name = model.Name;

            _departmentService.Update(dep);  // Update the department in the database

            return RedirectToAction("Index");
        }

       
        public IActionResult Delete(int id) 
        {
            _departmentService.Delete(id);
            return RedirectToAction("Index" , "Department");
        }

    }


}
