using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Service.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Salary { get; set; }
        public DateTime HiringDate { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageURL { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        public DepartmentDTO? Department { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
