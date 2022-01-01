using Entity.Concrete;
using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        User Login(UserForLoginDto userForLoginDto);
        User CorporateAccountRegister(CorporateAccountForRegisterDto corporateAccountForRegisterDto);
        User CustomerAccountRegister(CustomerAccountForRegisterDto customerAccountForRegisterDto);
    }
}
