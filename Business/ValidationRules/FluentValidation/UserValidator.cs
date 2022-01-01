using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Geçerli bir mail adresi girdiğinizden emin olunuz.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Parola boş geçilemez.");
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("Parola en az 8 karekterden oluşabilir.");
            RuleFor(x => x.Password).MaximumLength(30).WithMessage("Parola en fazla 30 karekterden oluşabilir.");
        }
    }
}
