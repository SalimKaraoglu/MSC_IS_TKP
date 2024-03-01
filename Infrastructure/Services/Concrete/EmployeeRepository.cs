using ApplicationCore.Entities.PersonEntities.Concrete;
using Infrastructure.Context;
using Infrastructure.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities.PersonEntities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Concrete
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<string> DepartmentNames(int employeeId)
        {
            var departments = await _context.Departments.Where(x => x.Status != Status.Passive && x.Id == employeeId).ToListAsync();

            var namesList = new List<string>();
            foreach (var department in departments)
            {
                namesList.Add(department.DepartmentName);
            }
            var departmentNames = string.Join(',', namesList);
            return departmentNames;
        }


        public async Task<bool> HasDepartment(int employeeId) => await _context.Departments.AnyAsync(x => x.Status != Status.Passive && x.Id == employeeId);
    }
}
