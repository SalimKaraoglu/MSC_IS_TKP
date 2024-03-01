using ApplicationCore.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTO_s.Account
{
    public class EditUserDTO
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur!")]
        [Display(Name = "Kullanıcı Adı")]
        [MinLength(3, ErrorMessage = "En az 3 karakter giriniz!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "E-mail zorunludur!")]
        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Lütfen bir email giriniz!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı zorunludur!")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        public EditUserDTO()
        {
            
        }

        public EditUserDTO(AppUser user)
        {
            UserName = user.UserName;
            Password = user.PasswordHash;
            Email = user.Email;
        }
    }
}
