using ApplicationCore.Entities.PersonEntities.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities.PersonEntities.Concrete
{
    public class Project : BaseEntity
    {
        public Project()
        {
            Employees = new List<Employee>();
        }

        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime? EndDate { get; set; }

        [NotMapped]
        public IFormFile? ProjectDocuments { get; set; }

        
        public List<Employee> Employees { get; set; }

    }
}
