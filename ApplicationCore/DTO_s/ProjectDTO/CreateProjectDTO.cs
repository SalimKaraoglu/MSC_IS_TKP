using ApplicationCore.DTO_s.DepartmentDTO;
using ApplicationCore.DTO_s.EmployeeDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTO_s.ProjectDTO
{
    public class CreateProjectDTO
    {
        [Display(Name = "Proje Adı")]
        public string? ProjectName { get; set; }

        [Display(Name = "Proje Açıklaması")]
        public string? ProjectDescription { get; set; }

        [Display(Name = "Proje Başlama Zamanı")]
        public DateTime? StartingDate { get; set; }
        
        [Display(Name = "Proje Sorumlusu")]
        public int? ProjectManager { get; set; }

        public List<ShowDepartmentDTO> Departments { get; set; }
        public List<ShowEmployeeDTO> Employees { get; set; }
    }
}
