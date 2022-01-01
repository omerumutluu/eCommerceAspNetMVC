using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICustomerAccountService
    {
        void Add(CustomerAccount customerAccount);
        void Update(CustomerAccount customerAccount);
        CustomerAccount GetByUserId(int userId);
        CustomerAccount GetByCustomerAccountId(int customerAccountId);
        List<CustomerAccount> GetAll();
    }
}
