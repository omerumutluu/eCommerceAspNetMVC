using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICartService
    {
        void Add(Cart cart);
        void Update(int id, Cart cart);
        void Delete(Cart cart);
        List<Cart> GetCartByUserId(int userId);
        Cart GetCartByUserIdAndProductId(int userId, int productId);
        Cart GetCartByCartId(int cartId);
    }
}
