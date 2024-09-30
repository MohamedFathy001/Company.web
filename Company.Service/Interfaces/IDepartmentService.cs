using Company.Service.DTO;

namespace Company.Service.Interfaces
{
    public interface IDepartmentService
    {
        DepartmentDTO GetbyId(int? id);
        IEnumerable<DepartmentDTO> GetAll();
        void Add(DepartmentDTO department);
        void Update(DepartmentDTO department);
        void Delete(int id);
    }
}
