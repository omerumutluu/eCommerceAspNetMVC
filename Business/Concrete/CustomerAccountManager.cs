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
    public class CustomerAccountManager : ICustomerAccountService
    {
        ICustomerAccountDal _customerAccountDal;

        public CustomerAccountManager(ICustomerAccountDal customerAccountDal)
        {
            _customerAccountDal = customerAccountDal;
        }

        public void Add(CustomerAccount customerAccount)
        {
            _customerAccountDal.Add(customerAccount);
        }

        public List<CustomerAccount> GetAll()
        {
            return _customerAccountDal.GetAll();
        }

        public CustomerAccount GetByCustomerAccountId(int customerAccountId)
        {
            return _customerAccountDal.Get(customerAccount => customerAccount.CustomerAccountId == customerAccountId);
        }

        public CustomerAccount GetByUserId(int userId)
        {
            return _customerAccountDal.Get(corporateAccount => corporateAccount.UserId == userId);
        }

        public void Update(CustomerAccount customerAccount)
        {
            _customerAccountDal.Update(customerAccount.CustomerAccountId, customerAccount);
        }
    }
}
