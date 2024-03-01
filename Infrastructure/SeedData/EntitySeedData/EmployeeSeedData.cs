using ApplicationCore.Entities.PersonEntities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SeedData.EntitySeedData
{
    public class EmployeeSeedData : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData
                 (
                   new Employee
                   {
                       Id = 1,
                       FirstName = "Personel - 1",
                       LastName = "Personel-1",
                       HireDate = new DateTime(1996, 01, 01),
                       Email = "Personel@test.com",
                       DepartmentId = 1,
                   },
                    new Employee
                    {
                        Id = 2,
                        FirstName = "Personel - 2",
                        LastName = "Personel-2",
                        HireDate = new DateTime(1996, 01, 01),
                        Email = "Personel-2@test.com",
                        DepartmentId = 1,
                    }
                );
        }
    }
}
