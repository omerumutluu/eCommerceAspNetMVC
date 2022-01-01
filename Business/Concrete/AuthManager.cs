using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.Repository.EntityFramework;
using Entity.Concrete;
using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        IUserDal _userDal;
        ICorporateAccountService _corporateAccountService = new CorporateAccountManager(new EfCorporateAccountDal());
        ICustomerAccountService _customerAccountService = new CustomerAccountManager(new EfCustomerAccountDal());
        public AuthManager(IUserDal userDal)
        {
            _userDal = userDal;
        }


        public User CorporateAccountRegister(CorporateAccountForRegisterDto corporateAccountForRegisterDto)
        {
            var userToAdd = new User { Email = corporateAccountForRegisterDto.Email, Password = corporateAccountForRegisterDto.Password };
            _userDal.Add(userToAdd);
            var userToAddInfo = _userDal.Get(user => user.Email == userToAdd.Email);
            var corporateAccountToAdd = new CorporateAccount
            {
                CorporateName = corporateAccountForRegisterDto.CorporateName,
                PhoneNumber = corporateAccountForRegisterDto.PhoneNumber,
                WebSite = corporateAccountForRegisterDto.WebSite,
                UserId = userToAddInfo.UserId,
                Role = "corporate",
                Status = true
            };

            _corporateAccountService.Add(corporateAccountToAdd);

            return userToAdd;            
        }

        public User CustomerAccountRegister(CustomerAccountForRegisterDto customerAccountForRegisterDto)
        {
            var userToAdd = new User { Email = customerAccountForRegisterDto.Email, Password = customerAccountForRegisterDto.Password };
            _userDal.Add(userToAdd);
            var currentUserInfo = _userDal.Get(user => user.Email == customerAccountForRegisterDto.Email);
            var customerAccountToAdd = new CustomerAccount
            {
                FirstName = customerAccountForRegisterDto.FirstName,
                LastName = customerAccountForRegisterDto.LastName,
                PhoneNumber = customerAccountForRegisterDto.PhoneNumber,
                UserId = currentUserInfo.UserId,
                Address = customerAccountForRegisterDto.Address,
                Role = "customer",
                Status = true
            };

            _customerAccountService.Add(customerAccountToAdd);

            return userToAdd;
        }

        public User Login(UserForLoginDto userForLoginDto)
        {
            return _userDal.Get(user => user.Email == userForLoginDto.Email && user.Password == userForLoginDto.Password);
        }
    }
}
