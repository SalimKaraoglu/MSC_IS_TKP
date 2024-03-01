using ApplicationCore.Entities.PersonEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Interface
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<bool> HasDepartment(int employeeId);
        Task<string> DepartmentNames(int employeeId);
    }
}
