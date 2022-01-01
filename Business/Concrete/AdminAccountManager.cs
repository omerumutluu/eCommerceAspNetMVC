using Business.Abstract;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AdminAccountManager : IAdminAccountService
    {
        IAdminAccountDal _adminAccountDal;

        public AdminAccountManager(IAdminAccountDal adminAccountDal)
        {
            _adminAccountDal = adminAccountDal;
        }

        public void Add(AdminAccount adminAccount)
        {
            _adminAccountDal.Add(adminAccount);
        }

        public List<AdminAccount> GetAllAdminAccounts()
        {
            return _adminAccountDal.GetAll();
        }

        public AdminAccount GetByUserId(int userId)
        {
            return _adminAccountDal.Get(adminAccount => adminAccount.UserId == userId);
        }
    }
}
