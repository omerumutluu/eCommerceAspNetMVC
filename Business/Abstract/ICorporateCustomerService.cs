using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICorporateAccountService
    {
        void Add(CorporateAccount corporateAccount);
        void Update(CorporateAccount corporateAccount);
        CorporateAccount GetByUserId(int userId);
        List<CorporateAccount> GetAll();
    }
}
