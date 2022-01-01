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
    public class OrderManager : IOrderService
    {
        IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public void Add(Order order)
        {
            _orderDal.Add(order);
        }

        public List<Order> GetByCorporateAccountId(int corporateAccountId)
        {
            return _orderDal.GetAll(order => order.Product.CorporateAccountId == corporateAccountId).OrderByDescending(order => order.OrderId).ToList();
        }

        public List<Order> GetByCustomerAccountId(int customerAccountId)
        {
            return _orderDal.GetAll(order => order.CustomerAccountId == customerAccountId).OrderByDescending(order => order.OrderId).ToList();
        }

        public Order GetByOrderId(int orderId)
        {
            return _orderDal.Get(order => order.OrderId == orderId);
        }

        public List<Order> GetCancelledOrdersByCorporateCustomer(int corporateCustomerId)
        {
            return _orderDal.GetAll(order => order.OrderStatus == "İptal edildi" && order.Product.CorporateAccount.CorporateAccountId == corporateCustomerId); ;
        }

        public List<Order> GetCompletedOrdersByCorporateCustomer(int corporateCustomerId)
        {
            return _orderDal.GetAll(order => order.OrderStatus == "Tamamlandı" && order.Product.CorporateAccount.CorporateAccountId == corporateCustomerId);
        }

        public List<Order> GetOnShippingOrdersByCorporateCustomer(int corporateCustomerId)
        {
            return _orderDal.GetAll(order => order.OrderStatus == "Kargoda" && order.Product.CorporateAccount.CorporateAccountId == corporateCustomerId);
        }

        public List<Order> GetPreparingOrdersByCorporateCustomer(int corporateCustomerId)
        {
            return _orderDal.GetAll(order => order.OrderStatus == "Hazırlanıyor" && order.Product.CorporateAccount.CorporateAccountId == corporateCustomerId);
        }

        public List<Order> GetWaitingConfirmOrdersByCorporateCustomer(int corporateCustomerId)
        {
            return _orderDal.GetAll(order => order.OrderStatus == "Onay Bekleniyor" && order.Product.CorporateAccount.CorporateAccountId == corporateCustomerId);
        }

        public void Update(Order order)
        {
            _orderDal.Update(order.OrderId, order);
        }
    }
}
