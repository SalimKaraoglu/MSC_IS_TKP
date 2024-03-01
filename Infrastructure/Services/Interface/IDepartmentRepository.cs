using ApplicationCore.Entities.PersonEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Interface
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
    }
}
