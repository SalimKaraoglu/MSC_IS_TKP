using ApplicationCore.Entities.PersonEntities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities.PersonEntities.Concrete
{
    public class Employee : BaseEntity
    {
        public Employee()
        {
            Projects = new List<Project>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime HireDate { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public List<Project> Projects { get; set; }
    }
}
