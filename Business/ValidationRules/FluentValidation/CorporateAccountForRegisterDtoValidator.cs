using Entity.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CorporateAccountForRegisterDtoValidator : AbstractValidator<CorporateAccountForRegisterDto>
    {
        public CorporateAccountForRegisterDtoValidator()
        {
            RuleFor(x => x.CorporateName).NotEmpty().WithMessage("Mağaza ismi boş geçilemez.");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon numarası boş geçilemez.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Mail bilgisi boş geçilemez.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Parola bilgisi boş geçilemez.");
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("Parola en az 8 karekterden oluşabilir.");
            RuleFor(x => x.Password).MaximumLength(30).WithMessage("Parola en fazla 30 karekterden oluşabilir.");

        }
    }
}
