using ApplicationCore.DTO_s.DepartmentDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.FluentValidators.DepartmentValidators
{
    public class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentDTO>
    {
        public UpdateDepartmentValidator()
        {
            Regex regex = new Regex("^[a-zA-Z- ığüşöçİĞÜŞÖÇ0123456789.]*$");

            RuleFor(x => x.DepartmentName)
                .NotEmpty()
                .WithMessage("Departman adı boş geçilemez!")
                .MinimumLength(3)
                .WithMessage("Minimum 3 karakter girmelisiniz!")
                .MaximumLength(100)
                .WithMessage("Maximum 100 karakter girmelisiniz!")
                .Matches(regex)
                .WithMessage("Sadece harf, rakam, boşluk, nokta ve '-' kullanabilirsiniz.");

            RuleFor(x => x.DepartmentDescription)
              .NotEmpty()
              .WithMessage("Departman açıklaması boş geçilemez!")
              .MinimumLength(3)
              .WithMessage("Minimum 3 karakter girmelisiniz!")
              .MaximumLength(200)
              .WithMessage("Maximum 200 karakter girmelisiniz!")
              .Matches(regex)
              .WithMessage("Sadece harf, rakam, boşluk, nokta ve '-' kullanabilirsiniz.");

      
        }
    }
}
