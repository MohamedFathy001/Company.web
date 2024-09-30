using Company.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Company.Data.Context
{
    //el IdentityUser feha attributes ktyra .... tab lw ana 3ayz azwd shwyt 7agat a3ml eh ?
    // aro7 a3ml file gded fel Entities asmeh msln ApplicationUser w y inherit men IdentityUser w 2a7ot el attributes
    // el gdida w agy hena a3ml keda " CompanyDbContext : IdentityDbContext<ApplicationUser> " 
    //badl dah  
    //public class CompanyDbContext : IdentityDbContext<IdentityUser>
    public class CompanyDbContext : IdentityDbContext<ApplicationUser>
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<DepartmentModel> Departments { get; set; }


    }
}
