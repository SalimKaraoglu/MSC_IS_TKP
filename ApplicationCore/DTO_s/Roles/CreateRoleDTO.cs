using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTO_s.Roles
{
    public class CreateRoleDTO
    {
        [Required(ErrorMessage = "Role Adı Zorunludur!")]
        [MinLength(3, ErrorMessage = "En az 3 karakter girmelisiniz!")]
        [Display(Name = "Role Adı")]
        public string RoleName { get; set; }
    }
}
