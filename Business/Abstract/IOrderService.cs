using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderService
    {
        void Add(Order order);
        void Update(Order order);
        Order GetByOrderId(int orderId);
        List<Order> GetByCorporateAccountId(int corporateAccountId);
        List<Order> GetByCustomerAccountId(int customerAccountId);
        List<Order> GetWaitingConfirmOrdersByCorporateCustomer(int corporateCustomerId);
        List<Order> GetPreparingOrdersByCorporateCustomer(int corporateCustomerId);
        List<Order> GetOnShippingOrdersByCorporateCustomer(int corporateCustomerId);
        List<Order> GetCompletedOrdersByCorporateCustomer(int corporateCustomerId);
        List<Order> GetCancelledOrdersByCorporateCustomer(int corporateCustomerId);
    }
}
