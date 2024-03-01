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
    public class DepartmentSeedData : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasData
             (
                 new Department
                 {
                     Id = 1,
                     DepartmentName = "Proje",
                     DepartmentDescription = "Karayolu Projesi Yapılmaktadır",
                 }
             );
        }
    }
}
