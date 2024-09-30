using AutoMapper;
using Company.Data.Entities;
using Company.Repository.Interfacies;
using Company.Service.DTO;
using Company.Service.Helper;
using Company.Service.Interfaces;

namespace Company.Service.Services
{
    public class EmployeeServices : IEmployeeService
    {
        public IUnitOfWork _UnitOfWork;
        private readonly IMapper _mapper;

        public EmployeeServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(EmployeeDTO employeeDTo)
        {
            //EmployeeModel employeeModel = new EmployeeModel()
            //{
            //    Name = employee.Name,
            //    Salary = employee.Salary,
            //    Address = employee.Address,
            //    Age = employee.Age,
            //    Email = employee.Email,
            //    HiringDate = employee.HiringDate,
            //    PhoneNumber = employee.PhoneNumber,
            //    ImageURL = employee.ImageURL,
            //    DepartmentId = employee.DepartmentId
            //};
            employeeDTo.ImageURL = DocumentSettings.Uploadfile(employeeDTo.Image, "Images");
            EmployeeModel employeeModel = _mapper.Map<EmployeeModel>(employeeDTo);
            _UnitOfWork.EmployeeRepo.Add(employeeModel);
            _UnitOfWork.Complete();
        }

        public void Delete(int id)
        {
            _UnitOfWork.EmployeeRepo.Delete(id);
            _UnitOfWork.Complete();
        }

        public IEnumerable<EmployeeDTO> GetAll()
        {
            var employees = _UnitOfWork.EmployeeRepo.GetAllWithDepartment(); // Updated method
            var employeeModel = _mapper.Map<IEnumerable<EmployeeDTO>>(employees);

            return employeeModel;
        }

        public EmployeeDTO GetbyId(int? id)
        {
            if (id is null)
            {
                return null;
            }
            var emp = _UnitOfWork.EmployeeRepo.GetbyId(id.Value);
            if (emp is null)
            {
                return null;
            }
            EmployeeDTO employee = _mapper.Map<EmployeeDTO>(emp);

            return employee;
        }

        public IEnumerable<EmployeeDTO> GetByName(string name)
        {
            var employeeName = _UnitOfWork.EmployeeRepo.GetByName(name);
            IEnumerable<EmployeeDTO> empName = _mapper.Map<IEnumerable<EmployeeDTO>>(employeeName);
           return empName;
        }

        public void Update(EmployeeDTO employeeDTo)
        {
            var existingEmployee = _UnitOfWork.EmployeeRepo.GetbyId(employeeDTo.Id);

            if (existingEmployee != null)
            {
                // Map the DTO to the existing model (this will update the properties).
                _mapper.Map(employeeDTo, existingEmployee);

                _UnitOfWork.EmployeeRepo.Updates(existingEmployee);
                _UnitOfWork.Complete();
            }
            else
            {
                // Handle the case where the employee does not exist (optional).
                throw new KeyNotFoundException($"Employee with ID {employeeDTo.Id} not found.");
            }
        }
    }
}
