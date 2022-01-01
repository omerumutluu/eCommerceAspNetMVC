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
    public class CorporateAccountManager : ICorporateAccountService
    {
        ICorporateAccountDal _corporateAccountDal;

        public CorporateAccountManager(ICorporateAccountDal corporateAccountDal)
        {
            _corporateAccountDal = corporateAccountDal;
        }

        public void Add(CorporateAccount corporateAccount)
        {
            _corporateAccountDal.Add(corporateAccount);
        }

        public List<CorporateAccount> GetAll()
        {
            return _corporateAccountDal.GetAll();
        }

        public CorporateAccount GetByUserId(int userId)
        {
            return _corporateAccountDal.Get(corporateAccount => corporateAccount.UserId == userId);
        }

        public void Update(CorporateAccount corporateAccount)
        {
            _corporateAccountDal.Update(corporateAccount.CorporateAccountId, corporateAccount);
        }
    }
}
