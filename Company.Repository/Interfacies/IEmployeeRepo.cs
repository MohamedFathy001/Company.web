using Company.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Interfacies
{
    public interface IEmployeeRepo : IGenericRepo<EmployeeModel>
    {
       IEnumerable<EmployeeModel> GetByName(string name);
        IEnumerable<EmployeeModel> GetAllWithDepartment();

    }
}
