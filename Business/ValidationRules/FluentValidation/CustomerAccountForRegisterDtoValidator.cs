using Entity.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CustomerAccountForRegisterDtoValidator : AbstractValidator<CustomerAccountForRegisterDto>
    {
        public CustomerAccountForRegisterDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Ad bilgisi boş geçilemez.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyad bilgisi boş geçilemez.");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon numarası boş geçilemez.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Adres bilgisi boş geçilemez.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Mail boş geçilemez.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Parola boş geçilemez.");
            RuleFor(x => x.Password).MinimumLength(4).WithMessage("Parola en az 8 karekterden oluşabilir.");
            RuleFor(x => x.Password).MaximumLength(30).WithMessage("Parola en fazla 30 karekterden oluşabilir.");
        }
    }
}
