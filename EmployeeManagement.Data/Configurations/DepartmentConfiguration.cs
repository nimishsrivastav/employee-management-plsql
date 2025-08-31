using EmployeeManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Data.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("DEPARTMENTS");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(d => d.Name)
                .HasColumnName("NAME")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(d => d.Description)
                .HasColumnName("DESCRIPTION")
                .HasMaxLength(500);

            builder.Property(d => d.CreatedDate)
                .HasColumnName("CREATED_DATE")
                .HasDefaultValueSql("SYSDATE");

            // Seed data
            builder.HasData(
                new Department { Id = 1, Name = "IT", Description = "Information Technology", CreatedDate = DateTime.Now },
                new Department { Id = 2, Name = "HR", Description = "Human Resources", CreatedDate = DateTime.Now },
                new Department { Id = 3, Name = "Finance", Description = "Finance Department", CreatedDate = DateTime.Now }
            );
        }
    }
}
