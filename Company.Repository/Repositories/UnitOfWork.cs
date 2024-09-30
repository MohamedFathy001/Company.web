using Company.Data.Context;
using Company.Repository.Interfacies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyDbContext _db;
        public IDepartmentRepo DepartmentRepo { get; set; }
        public IEmployeeRepo EmployeeRepo { get ; set ; }
        public UnitOfWork(CompanyDbContext db) 
        {
            _db = db;
            DepartmentRepo = new DepartmentRepo(db);
            EmployeeRepo = new EmployeeRepo(db);    
        }


        public int Complete()
        {
            return _db.SaveChanges();
        }
    }
}
