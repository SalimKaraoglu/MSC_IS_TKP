using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTO_s.Account
{
    public class LogInDTO
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur!")]
        [Display(Name = "Kullanıcı Adı")]
        [MinLength(3, ErrorMessage = "En az 3 karakter giriniz!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre alanı zorunludur!")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
    }
}
