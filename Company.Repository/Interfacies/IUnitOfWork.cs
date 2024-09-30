using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Interfacies
{
    public interface IUnitOfWork
    {
        public IDepartmentRepo DepartmentRepo { get; set; }

        public IEmployeeRepo EmployeeRepo { get; set; }

        int Complete();
    }
}
