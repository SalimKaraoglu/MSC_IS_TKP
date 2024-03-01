using ApplicationCore.DTO_s.ProjectDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.FluentValidators.ProjectValidators
{
    public class CreateProjectValidator : AbstractValidator<CreateProjectDTO>
    {
        public CreateProjectValidator()
        {
            // Ad girileceği zaman harf giremesin diye 
            Regex regex = new Regex("^[a-zA-Z- ığüşöçİĞÜŞÖÇ0123456789]*$");

            RuleFor(x => x.ProjectName)
                .NotEmpty()
                .WithMessage("Proje Adı Boş Geçilemez!")
                .MinimumLength(3)
                .WithMessage("Minimum 3 karakter Giriniz!")
                .MaximumLength(200)
                .WithMessage("Maximum 200 karakter Girebilirsiniz!")
                .Matches(regex)
                .WithMessage("Sadece harf, rakam, boşluk, nokta ve '-' kullanabilirsiniz!");

            RuleFor(x => x.ProjectDescription)
                .NotEmpty()
                .WithMessage("Proje Açıklması Zorunludur!")
                .MinimumLength(3)
                .WithMessage("Minimum 3 karakter Giriniz!")
                .MaximumLength(300)
                .WithMessage("Maximum 300 karakter Girebilirsiniz!");

            RuleFor(x => x.StartingDate)
                .NotEmpty()
                .WithMessage("Proje Başlangıç Tarihini Boş Geçemezsiniz!");


            RuleFor(x => x.ProjectManager)
                .NotEmpty()
                .WithMessage("Proje Sorumlusu Geçilemez!");


        }
    }
}
