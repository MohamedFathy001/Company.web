using Company.Service.DTO;

namespace Company.web.Controllers
{
    public class EmployeeDetailsViewModel
    {
        public EmployeeDTO Employee { get; set; }
        public IEnumerable<DepartmentDTO> Departments { get; set; }
    }
}