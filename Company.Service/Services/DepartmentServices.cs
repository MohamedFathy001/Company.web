 using AutoMapper;
using Company.Data.Entities;
using Company.Repository.Interfacies;
using Company.Service.DTO;
using Company.Service.Interfaces;

namespace Company.Service.Services
{
    public class DepartmentServices : IDepartmentService
    {
        public IUnitOfWork _UnitOfWork;
        private readonly IMapper _mapper;

        public DepartmentServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<DepartmentDTO> GetAll()
        {
            var departments = _UnitOfWork.DepartmentRepo.GetAll();
            IEnumerable<DepartmentDTO> departmentModel = _mapper.Map<IEnumerable<DepartmentDTO>>(departments);

            return departmentModel;
        }

        public DepartmentDTO GetbyId(int? id)
        {
            if(id is null)
            {
                return null;
            }
            var dep = _UnitOfWork.DepartmentRepo.GetbyId(id.Value);
            if(dep is null)
            {
                return null;
            }
            DepartmentDTO depDto = _mapper.Map<DepartmentDTO>(dep);

            return depDto;
        }

        public void Add(DepartmentDTO departmentDTo)
        {
            DepartmentModel deptModel = _mapper.Map<DepartmentModel>(departmentDTo);
            _UnitOfWork.DepartmentRepo.Add(deptModel);
            _UnitOfWork.Complete();
        }

        public void Update(DepartmentDTO departmentDTo)
        {
            var existingDepartment = _UnitOfWork.DepartmentRepo.GetbyId(departmentDTo.Id);

            if (existingDepartment != null)
            {
                // Map the DTO to the existing model (this will update the properties).
                _mapper.Map(departmentDTo, existingDepartment);

                _UnitOfWork.DepartmentRepo.Updates(existingDepartment);
                _UnitOfWork.Complete();
            }
            else
            {
                // Handle the case where the employee does not exist (optional).
                throw new KeyNotFoundException($"Employee with ID {departmentDTo.Id} not found.");
            }
        }

        public void Delete(int id)
        {
            _UnitOfWork.DepartmentRepo.Delete(id);
            _UnitOfWork.Complete();
        }

    }
}
