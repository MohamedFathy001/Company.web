using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Data.Entities
{
    public class EmployeeModel : BaseEntity 
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Salary { get; set; }
        public DateTime HiringDate { get; set; }
        public string ImageURL { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        public DepartmentModel Department { get; set; }
    }
}
