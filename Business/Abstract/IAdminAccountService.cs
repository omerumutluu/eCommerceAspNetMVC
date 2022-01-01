using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAdminAccountService
    {
        void Add(AdminAccount adminAccount);
        AdminAccount GetByUserId(int userId);
        List<AdminAccount> GetAllAdminAccounts();
    }
}
