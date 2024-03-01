using ApplicationCore.Entities.PersonEntities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities.PersonEntities.Concrete
{
    public class Department : BaseEntity
    {
        public Department()
        {
            Employees = new List<Employee>();
        }

        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentDescription { get; set; }

     
        public List<Employee> Employees { get; set; }
    }
}
