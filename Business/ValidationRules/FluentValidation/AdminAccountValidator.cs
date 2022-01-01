using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class AdminAccountValidator : AbstractValidator<AdminAccount>
    {
        public AdminAccountValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Ad bilgisi boş geçilemez.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyad bilgisi boş geçilemez.");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon numarası boş geçilemez.");
        }
    }
}
