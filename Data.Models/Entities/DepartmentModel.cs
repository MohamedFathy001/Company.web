using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Data.Entities
{
    public class DepartmentModel : BaseEntity
    {
        public string Name { get; set; }
        public string code { get; set; }

        public ICollection<EmployeeModel> Employees { get; set; } = new List<EmployeeModel>();
    }
}
