using ApplicationCore.Entities.PersonEntities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTO_s.DepartmentDTO
{
    public class CreateDepartmentDTO
    {
        [Display(Name = "Departman Adı")]
        public string? DepartmentName { get; set; }

        [Display(Name = "Departman Açıklama")]
        public string? DepartmentDescription { get; set; }

    }
}
