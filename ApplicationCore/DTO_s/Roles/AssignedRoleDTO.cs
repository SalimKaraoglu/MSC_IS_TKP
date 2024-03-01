using ApplicationCore.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTO_s.Roles
{
    public class AssignedRoleDTO
    {
        public IdentityRole Role { get; set; }
        public string RoleName { get; set; }

        public List<AppUser>? HasRole { get; set; }  // Bu role sahip olanların listesi
        public List<AppUser>? HasNotRole { get; set; }  // Bu role sahip olmayanlar listesi

        public string[]? AddIds { get; set; }  // Bu role eklenecek kişilerin Id listesi
        public string[]? DeleteIds { get; set; } // Bu rolden çıkarılacakların Id listesi
    }
}
