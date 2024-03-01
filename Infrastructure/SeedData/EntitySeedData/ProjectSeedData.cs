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
    public class ProjectSeedData : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasData
                (
                 new Project
                 {
                     Id = 1,
                     ProjectName = "Proje-1",
                     ProjectDescription = "Proje Açıklama",
                     StartingDate = new DateTime(1996, 02, 02),
                 }
                );
        }
    }
}
