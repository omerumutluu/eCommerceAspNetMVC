using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CustomerAccountValidator : AbstractValidator<CustomerAccount>
    {
        public CustomerAccountValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Ad alanı boş geçilemez");       
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyad alanı boş geçilemez");       
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon numarası boş geçilemez");       
            RuleFor(x => x.Address).NotEmpty().WithMessage("Adres bilgisi boş geçilemez");       
        }
    }
}
