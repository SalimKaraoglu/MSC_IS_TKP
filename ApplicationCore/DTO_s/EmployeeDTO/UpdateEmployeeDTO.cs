using ApplicationCore.DTO_s.DepartmentDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTO_s.EmployeeDTO
{
    public class UpdateEmployeeDTO
    {
        public int Id { get; set; }
        [Display(Name = "Ad")]
        public string? FirstName { get; set; }

        [Display(Name = "Soyad")]
        public string? LastName { get; set; }

        [Display(Name = "E-mail")]
        public string? Email { get; set; }

        [Display(Name = "İşe Giriş Tarihi")]
        public DateTime? HireDate { get; set; }

        [Display(Name ="Departman İsmi")]
        public int? DepartmentId { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public List<ShowDepartmentDTO>? Departments { get; set; }
    }
}
