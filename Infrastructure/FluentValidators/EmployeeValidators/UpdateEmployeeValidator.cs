using ApplicationCore.DTO_s.EmployeeDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.FluentValidators.EmployeeValidators
{
    public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeDTO>
    {
        public UpdateEmployeeValidator()
        {
            // Ad girileceği zaman harf giremesin diye 
            Regex regex = new Regex("^[a-zA-Z- ığüşöçİĞÜŞÖÇ0123456789]*$");

            RuleFor(x => x.FirstName)
                    .NotEmpty()
                    .WithMessage("Ad alanı zorunludur!")
                    .MinimumLength(3)
                    .WithMessage("En az 3 karakter girmelisiniz!")
                    .MaximumLength(100)
                    .WithMessage("En fazla 100 karakter girebilirsiniz!")
                    .Matches(regex)
                    .WithMessage("Sadece harf girebilirisiniz!");

            RuleFor(x => x.LastName)
                    .NotEmpty()
                    .WithMessage("Soyad alanı zorunludur!")
                    .MinimumLength(2)
                    .WithMessage("En az 2 karakter girmelisiniz!")
                    .MaximumLength(200)
                    .WithMessage("En fazla 200 karakter girebilirsiniz!")
                    .Matches(regex)
                    .WithMessage("Sadece harf girebilirisiniz!");

            RuleFor(x => x.Email)
                    .NotEmpty()
                    .WithMessage("Email alanı zorunludur!")
                    .EmailAddress()
                    .WithMessage("Lütfen bir email adresi giriniz!");

            RuleFor(x => x.HireDate)
                    .NotEmpty()
                    .WithMessage("İşe Giriş Tarihi Zorunludur!");

            RuleFor(x => x.DepartmentId)
                    .NotEmpty()
                    .WithMessage("Departman boş geçilemez!");
        }
    }
}
