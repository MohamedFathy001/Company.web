    using Company.Data.Context;
using Company.Data.Entities;
using Company.Repository.Interfacies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Repositories
{
    public class DepartmentRepo : GenericRepository<DepartmentModel> ,  IDepartmentRepo
    {
        private readonly CompanyDbContext _context;
        public DepartmentRepo(CompanyDbContext context) : base(context) 
        {
            _context = context;
        }
    }
}
