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
    public class CommentManager : ICommentService
    {
        ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public void Add(Comment comment)
        {
            _commentDal.Add(comment);
        }

        public List<Comment> GetByProductId(int productId)
        {
            return _commentDal.GetAll(comment => comment.ProductId == productId).OrderByDescending(comment => comment.CommentId).ToList();
        }

        public List<Comment> GetByUserId(int userId)
        {
            return _commentDal.GetAll(comment => comment.UserId == userId).OrderByDescending(comment => comment.CommentId).ToList() ;
        }

        public List<Comment> GetCommentsByCorporateCustomerId(int corporateCustomerId)
        {
            return _commentDal.GetAll(comment => comment.Product.CorporateAccountId == corporateCustomerId).OrderByDescending(comment => comment.CommentId).ToList();
        }
    }
}
