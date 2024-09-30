using Company.Data.Context;
using Company.Data.Entities;
using Company.Repository.Interfacies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Repositories
{
    public class EmployeeRepo : GenericRepository<EmployeeModel> ,  IEmployeeRepo
    {
        // me7tag deh lma yeb2a mawgod methods t7t zay ely mawgoda deh GetByName
        private readonly CompanyDbContext _context;
        public EmployeeRepo(CompanyDbContext Context) : base(Context)
        {
            _context = Context;
        }

        public IEnumerable<EmployeeModel> GetAllWithDepartment()
        {
            return _context.Employees.Include(e => e.Department).ToList();
        }

        public IEnumerable<EmployeeModel> GetByName(string name)
        {
           return _context.Employees.Where(x => 
           x.Name.Trim().ToLower().Contains(name.Trim().ToLower()) ||
           x.Email.Trim().ToLower().Contains(name.Trim().ToLower())       
           ).ToList();
        }
    } 
}
