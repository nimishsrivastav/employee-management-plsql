using EmployeeManagement.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("EMPLOYEES");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.FirstName)
                .HasColumnName("FIRST_NAME")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.LastName)
                .HasColumnName("LAST_NAME")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Email)
                .HasColumnName("EMAIL")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.Salary)
                .HasColumnName("SALARY")
                .HasColumnType("NUMBER(10,2)");

            builder.Property(e => e.HireDate)
                .HasColumnName("HIRE_DATE")
                .HasColumnType("DATE");

            builder.Property(e => e.CreatedDate)
                .HasColumnName("CREATED_DATE")
                .HasDefaultValueSql("SYSDATE");

            builder.Property(e => e.ModifiedDate)
                .HasColumnName("MODIFIED_DATE");

            builder.Property(e => e.DepartmentId)
                .HasColumnName("DEPARTMENT_ID");

            // Foreign key relationship
            builder.HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Unique constraint on email
            builder.HasIndex(e => e.Email).IsUnique();
        }
    }
}
