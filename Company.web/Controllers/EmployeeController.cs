using Company.Service.DTO;
using Company.Service.Helper;
using Company.Service.Interfaces;
using Company.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _empployeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService employeeService , IDepartmentService departmentService) 
        {
            _empployeeService = employeeService;
            _departmentService = departmentService;
        }


        public IActionResult Index(string searchInp)
        {
            if (searchInp != null)
            {
                var emp = _empployeeService.GetByName(searchInp);
                return View(emp);
            }
            var employees = _empployeeService.GetAll(); 
            return View(employees);
        }


        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = _departmentService.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _empployeeService.Add(model);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }


        public IActionResult Details(int? id)
        {
            var emp = _empployeeService.GetbyId(id);

            if (emp is null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }
            var departments = _departmentService.GetAll();
            var viewModel = new EmployeeDetailsViewModel
            {
                Employee = emp,
                Departments = departments
            };

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult Update(EmployeeDetailsViewModel model)
        {
            var employee = _empployeeService.GetbyId(model.Employee.Id);
            if (employee == null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }

            employee.Age = model.Employee.Age;
            employee.Address = model.Employee.Address;
            employee.PhoneNumber = model.Employee.PhoneNumber;
            employee.Salary = model.Employee.Salary;
            employee.Name = model.Employee.Name;
            employee.Email = model.Employee.Email;
            employee.HiringDate = model.Employee.HiringDate;
            employee.DepartmentId = model.Employee.DepartmentId;

            if (model.Employee.Image != null)
            {
                var newImageUrl = DocumentSettings.Uploadfile(model.Employee.Image, "Images");
                employee.ImageURL = newImageUrl;
            }

            _empployeeService.Update(employee);
            return RedirectToAction("Index");
        }



        public IActionResult Delete(int id)
        {
            _empployeeService.Delete(id);
            return RedirectToAction("Index" , "Employee");
        }


    }
}
