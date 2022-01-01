using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CorporateAccountValidator : AbstractValidator<CorporateAccount>
    {
        public CorporateAccountValidator()
        {
            RuleFor(x => x.CorporateName).NotEmpty().WithMessage("Mağaza adı boş geçilemez.");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon numarası boş geçilemez.");
        }
    }
}
