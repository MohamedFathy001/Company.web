using Company.Data.Entities;
using Company.Service.DTO;

namespace Company.Service.Interfaces
{
    public interface IEmployeeService
    {
        EmployeeDTO GetbyId(int? id);
        IEnumerable<EmployeeDTO> GetAll();
        void Add(EmployeeDTO employeeDTo);
        void Update(EmployeeDTO employeeDTo);
        void Delete(int id);
        IEnumerable<EmployeeDTO> GetByName(string name);

    }
}
