using Company.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.DTO
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string code { get; set; }
        public DateTime CreateAt { get; set; }

        public ICollection<EmployeeDTO> Employees { get; set; } = new List<EmployeeDTO>();
    }
}
