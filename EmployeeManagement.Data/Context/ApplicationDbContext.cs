using EmployeeManagement.Data.Configurations;
using EmployeeManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());

            // Create stored procedures
            CreateStoredProcedures(modelBuilder);
        }

        private void CreateStoredProcedures(ModelBuilder modelBuilder)
        {
            // Create stored procedure for getting employee with department
            modelBuilder.Entity<Employee>().ToTable(tb => tb.HasTrigger("EMPLOYEE_UPDATE_TRIGGER"));

            // Add custom SQL for stored procedures
            modelBuilder.HasDbFunction(typeof(ApplicationDbContext).GetMethod(nameof(GetEmployeesByDepartment))!)
                .HasName("GET_EMPLOYEES_BY_DEPT");
        }

        // Method for calling stored procedure
        public IQueryable<Employee> GetEmployeesByDepartment(int departmentId)
            => FromExpression(() => GetEmployeesByDepartment(departmentId));
    }
}
