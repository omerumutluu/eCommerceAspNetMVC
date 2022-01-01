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
    public class CartManager : ICartService
    {
        private ICartDal _cartDal;

        public CartManager(ICartDal cartDal)
        {
            _cartDal = cartDal;
        }

        public void Add(Cart cart)
        {
            _cartDal.Add(cart);
        }

        public void Delete(Cart cart)
        {
            _cartDal.Delete(cart);
        }

        public Cart GetCartByCartId(int cartId)
        {
            return _cartDal.Get(cart => cart.CartId == cartId);
        }

        public List<Cart> GetCartByUserId(int userId)
        {
            return _cartDal.GetAll(cart => cart.UserId == userId);
        }

        public Cart GetCartByUserIdAndProductId(int userId, int productId)
        {
            return _cartDal.Get(cart => cart.UserId == userId && cart.ProductId == productId);
        }

        public void Update(int id, Cart cart)
        {
            _cartDal.Update(id, cart);
        }
    }
}
