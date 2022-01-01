using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICommentService
    {
        void Add(Comment comment);
        List<Comment> GetByProductId(int productId);
        List<Comment> GetByUserId(int userId);
        List<Comment> GetCommentsByCorporateCustomerId(int corporateCustomerId);
    }
}
