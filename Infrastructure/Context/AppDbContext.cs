using ApplicationCore.Entities.Concrete;
using ApplicationCore.Entities.PersonEntities.Concrete;
using Infrastructure.SeedData.EntitySeedData;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) => AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }


        public AppDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
            Database.Migrate();
            //Sizin yerinize otomatik olarak update-database komutunu çağırır.
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EmployeeSeedData());
            modelBuilder.ApplyConfiguration(new DepartmentSeedData());
            modelBuilder.ApplyConfiguration(new ProjectSeedData());

        }
    }
}
